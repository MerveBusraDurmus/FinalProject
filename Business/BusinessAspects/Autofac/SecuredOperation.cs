using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Core.Extensions;
using Business.Constants;

namespace Business.BusinessAspects.Autofac
{
    //JWT
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;  //yapılan her istek için httpcontext oluşur.

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(','); //metni virgüle göre ayırıp array'e atacak.
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>(); //constructor injection yapmadık. constructor'a ihtiyaç duyulmadığı zamanlar bu kullanılacak.Direk injection yapabilmek için kullanırız.

        }

        protected override void OnBefore(IInvocation invocation)  //metodun önünde çalıştır.
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();  //kullanıcının rollerini bul.
            foreach (var role in _roles)  //kullanıcının rollerini gez. claimlerin içerisinde ilgili rol varsa return et.
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);  //yoksa mesajı döndür.
        }
    }
}
