using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }  //Web API'deki appsettings.json(configuration dosyası) içerisini okumamız için 
        private TokenOptions _tokenOptions;  //okuduklarımızı bir nesneye atmamız için 
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration) //configuration'ı .net core veriyor.
        {
            //configuration=appsetting 
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>(); //appsettings içerisindeki Tokenoptions alanını bul.(tokenoptions alanı) ve onu TokenOptions sınıfının değerlerini kullanarak maple.

        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);  //jwt oluşturduk
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);  //Jwt token nesnesini WriteToken ile string değere çevirdik.

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}"); //Başına $ işareti eklersen çift tırnak içerisine kod yazabilirsin. + eklemek yerine iki stringi yan yana getirmiş olursun.
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;


            //Extension=genişletmek , var olan bir nesneye yeni metodlar ekleyebiliriz. Buna extension denir.Extension metod yazabilmek için hem class'ın hem metodun static olması gerekir.
        }
    }
}
