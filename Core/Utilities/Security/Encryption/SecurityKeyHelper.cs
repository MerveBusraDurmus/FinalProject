using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    //içerisinde şifreleme olan sistemlerde herşeyi byte[] formatında veriyor olmamız gerekiyor.
    public class SecurityKeyHelper
    {
        //SecurityKey : Token'ı oluştururken asp.net'in oluşturacağı anahtarın ismi.
        public static SecurityKey CreateSecurityKey(string securityKey)  //SecurityKey IdentityModel.Tokens dan geliyor.
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
