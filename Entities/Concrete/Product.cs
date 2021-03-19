
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    //class ların default erişim belirleyicisi internal demektir. sadece içinde bulunduğu sınıftan erişilir.
    public class Product:IEntity //public ile class a diğer katmanların da ulaşabilmesini sağlıyoruz. 
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public short UnitsInStock { get; set; }  //short int in bir küçüğü, veri tabanında smallint olarak tutulur.
        public decimal UnitPrice { get; set; }
        
    }
}
