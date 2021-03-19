using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult:Result
    {
        public SuccessResult(string message) : base(true, message)  //base derken Resulttan bahsediyoruz. tek veya iki parametre gönderebiliriz.
        {

        }

        public SuccessResult():base(true)
        {

        }
    }
}
