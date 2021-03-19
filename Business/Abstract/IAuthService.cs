using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IAuthService
    {
        //authantication dosyasında kullanıcının bir sisteme kayıt olması , giriş yapması işlemleri vardır.
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password); //kayıt
        IDataResult<User> Login(UserForLoginDto userForLoginDto); //giriş
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
