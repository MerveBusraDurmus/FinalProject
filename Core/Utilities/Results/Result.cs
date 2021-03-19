using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
      

        public Result(bool success, string message):this(success)  //result'ın tek parametreli constructor ına success'ide göndeririz.Bu satırı çalıştıran birisi alttaki constructor'ı da çalıştırmış olur. İkisi de aynı anda çalışır.Sadece success çalıştırmak istediğimizde alttaki constructor çalışır.C# da this demek class'ın kendisi demek.[this(success)]--Result'ın tek parametreli olan constructor'ına success'i yolla demek.
        {
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
        }


        public bool Success { get; }  //getter readonly dir, okunabilirdir. readonly'ler constructor da set edilebilir. 

        public string Message { get; }
    }
}
