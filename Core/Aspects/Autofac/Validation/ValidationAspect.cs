using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception //Aspect
    {
        private Type _validatorType;  //field
        public ValidationAspect(Type validatorType)  //constructor
        {
            //Defensive coding (savunma odaklı kodlama)
            if (!typeof(IValidator).IsAssignableFrom(validatorType))  //validatorType bir IValidator mı ,constructordan gelen validatorType atanabilir mi?
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil.");
            }

            _validatorType = validatorType;  //eğer atanabilirse constructordan gelen validatorType'ı _validatorType'a at.
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);  //çalışma anında instance oluşturur.activator.createInstance = reflection kodu.bu satır productValidator'ı newledi.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; //ProductValidator'ın basetype'ına git. Onunda generic parametresinin 0. olanın tipini yakala(product)
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); //metodun argümanlarını gez. oradaki tip entityType' a eşitse.
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
