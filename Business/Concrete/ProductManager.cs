using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        //bir iş sınıfı başka sınıfları newlemez.
        IProductDal _productDal;  //injection yapmamız gerekir.
        ICategoryService _categoryService;


        //constructor
        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        { 
            _productDal = productDal;
            _categoryService = categoryService;
            
        }

        //claim
        [SecuredOperation("product.add, admin,editor")]  //operation deyince aklına metod gelsin 
        [ValidationAspect(typeof(ProductValidator))]  //Attribute tiplerini bu şekilde atarız.
        public IResult Add(Product product)
        {
            //business code
            //validation -- doğrulama -- fluent validation
            //ValidationTool.Validate(new ProductValidator(), product);
            //Eğer mevcut kategori sayısı 15'i geçtiyse sisteme yeni ürün eklenemez.
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName), CheckIfProductCountOfCategoryCorrect(product.CategoryId),CheckIfCategoryLimitExceded());

            if (result!=null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);

        }

        public IDataResult<List<Product>> GetAll()
        {
            //iş kodları 
            //Yetkisi var mı ?
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
           
            throw new NotImplementedException();
        }


        //iş kuralı parçacığı
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId) //Categorydeki ürün sayısının kurallara uygunluğunu doğrula.
        {
            //bir kategoride en fazla 10 ürün olabilir.
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;  //Select count(*) from products where categoryId=1
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }


            return new SuccessResult();
        }


        private IResult CheckIfProductNameExists(string productName)  //aynı ürün ismi daha önce eklenmiş mi
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any(); //any var mı demek. any bool döndürür.
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }


        private IResult CheckIfCategoryLimitExceded() //categoryManager içerisinde yazarsak başlı başına bir servis olur. burası categoryService'i kullanan bir ürünün onu nasıl ele aldığıyla ilgilidir.
        {
            var result = _categoryService.GetAll(); 
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }
    }
}
