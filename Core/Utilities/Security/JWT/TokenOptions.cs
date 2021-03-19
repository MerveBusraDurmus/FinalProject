using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class TokenOptions
    {
        //Web API içerisindeki değerlerin nesnelerini oluşturduk.TokenOptions appsettings'deki alan'a verdiğimiz isim.
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
