using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        //Web Apinin kullanacağı JWT'larının oluşturulabilmesi için (Credentials= bir sisteme girmek için elimizde olanlar, kullanıcı adı , şifre. Kimlik bilgileri diyebiliriz.) elimizdeki key'i vericez ve bize imzalama nesnesini döndürecek.
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
