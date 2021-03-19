using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user,List<OperationClaim> operationClaims);

        //kullanıcı kullanıcı adı ve parolayı girip login dediği anda bilgiler doğruysa çalışacak. İlgili kullanıcı için veritabanına gidecek bu kullanıcının claimlerini bulacak.Orada JWT üretecek onları istek yapılan kısma gönderecek.
    }


}
