using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        //verdiğimiz password değerine göre salt ve hash değerlerini oluşturmaya çalışıyoruz.
        //password dışarıdan gelen değer. passwordHash ve passwordSalt oluşturulacak değerler.
        public static void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt) //out olanlar dışarı çıkacaklar.out boş gönderilen değeri doldurup geri döndürüyordu.
        {
            //disposable pattern (using kullanmamızın nedeni garbage collector gerek kalmadan işimiz bitince bağlantıyı bitirmek.)
            using (var hmac = new System.Security.Cryptography.HMACSHA512())  //.net'in Cryptography sınıfında yararlanacağız.seçtiğimiz algoritmaya göre hash değerimizi oluşturacağız.
            {
                passwordSalt = hmac.Key;  //key algoritmanın değişmeyen anahtarıdır. şifreyi çözerken buna ihtiyaç vardır.hmac içerisindeki key değerini kullanıyoruz. Her kullanıcı için farklı key değeri oluşturur.Bu nedenle güvenlidir.
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); //bir string'in byte karşılığını almak için kullanırız.
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt) //out yok çünkü veritabanı ve kullanıcının gönderdiğini karşılaştırıyoruz.Kullanıcının gönderdiği password'ü yukarıdaki algoritmayı kullanarak aynı şekilde hash'leseydin karşına aynı passwordHash çıkar mıydı çıkmaz mıydı bunun kontrolünü yapıyoruz.
        {
            //disposable pattern
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                //computed=hesaplanan
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i]!=passwordHash[i]) //hesaplanan değer ile veritabanından gelen değer birbirine eşit değilse
                    {
                        return false;
                    }
                }
                return true;
            }

            
        }
    }
}
