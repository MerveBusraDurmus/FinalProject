using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products; //naming collection  . global tanımlama. alt çizgi ile başlar.

        public InMemoryProductDal()  //bellekte referans alındığı zaman çalışacak olan blok
        {
            //bu sınıf newlendiği zaman yapılacaklar.
            //oracle,sql server,postgres,mongodb den geliyormuş gibi simüle ediyoruz.
            _products = new List<Product> 
            {
                new Product{ProductId=1,CategoryId=1,ProductName="Bardak",UnitPrice=15,UnitsInStock=15},
                new Product{ProductId=2,CategoryId=1,ProductName="Kamera",UnitPrice=500,UnitsInStock=3},
                new Product{ProductId=3,CategoryId=2,ProductName="Telefon",UnitPrice=1500,UnitsInStock=2},
                new Product{ProductId=4,CategoryId=2,ProductName="Klavye",UnitPrice=150,UnitsInStock=65},
                new Product{ProductId=5,CategoryId=2,ProductName="Fare",UnitPrice=85,UnitsInStock=1}
            };

           
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            //_products.Remove(product); //bu şekilde silinmez.çünkü arayüzden gönderdiğimiz product heap bölgesinde 5 tane adresi var. referans numaraları aynı olmadığı için silemez.tüm bilgileri aynı olsada referans numaraları farklı olacağı için silmez.
            //Product productToDelete = null;  //Product productToDelete = new Product(); şeklinde yazarsak hatalı olur.
            //foreach (var p in _products)
            //{
            //    if (product.ProductId==p.ProductId)
            //    {
            //        productToDelete = p;  //silinecek eleman dışarıdan gönderilen productıd ve elimdeki productıd eşit olan elemandır.
            //    }
            //}

            //yukarıdaki kodun LINQ ile yazılmış hali;
            Product productToDelete = _products.SingleOrDefault(p=>p.ProductId==product.ProductId);  //singleordefault tek bir eleman bulmaya yarar. //Lambda--- yukarıdaki foreach işini yapar.
            _products.Remove(productToDelete);


            //LINQ - Language Integrated Query -Dile gömülü sorgulama

        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
           return  _products.Where(p => p.CategoryId == categoryId).ToList();  //where koşulundaki şarta uyan tüm elemanları yeni bir liste haline getirip döndürür.&& ile istediğimiz kadar koşul ekleyebiliriz.
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId); //gönderdiğim productId sine eşit olan listedeki productId yi bul.
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }
    }
}
