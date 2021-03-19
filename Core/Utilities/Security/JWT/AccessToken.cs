using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    //erişim anahtarı
    //AccessToken anlamsız karakterlerden oluşan bir anahtar değeridir.
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; } //bitiş zamanı

        //kullanıcı Web API üzerinden bilgilerini vericek. Bizde ona Token ve biteceği süreyi vericez.
        //kullanıcı sisteme login olduğu zaman login olan kullanıcının login olma bilgilerini kontrol edicez. Eğer kullanıcının login olmak istediği bilgiler geçerliyse bu kullanıcıya bir token veriyor olacağız.Kullanıcı bu token'ı hiç bir şekilde değişteremez.Kullanıcıya token verdikten sonra kullanıcı yapacağı istekleri bu token vasıtasıyla gerçekleştirecek.Bizde bu token'a göre, kullanıcının yetkilerine göre,Token'ın geçerliliğine göre işlemi yapıp yapmamasına izin veriyor olacağız. Token işlemleri evrensel işlemler.Bütün projelerde bu token yapısını kullanabiliriz.Bu nedenle Core katmanında.
    }
}
