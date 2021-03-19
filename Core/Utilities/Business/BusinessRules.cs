using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {

        //iş motoru. ProductManager da yazdığımız iş parçacıklarını iş motoru ile çalıştırıyoruz.
        public static IResult Run(params IResult[] logics)  //params ile istediğimiz kadar IResult verebiliyoruz. 
        {
            foreach (var logic in logics)   //bütün kuralları gez. uymayan kuralı döndür.
            {
                if (!logic.Success)
                {
                    return logic;
                }
            }
            return null;
        }
    }
}
