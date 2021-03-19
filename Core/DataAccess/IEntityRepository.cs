using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    //generic constraint (generic kısıt)
    //class : referans tip
    //IEntity : IEntity olabilir veya IEntity implemente eden bir nesne olabilir.
    //new() : new'lenebilir olmalı.
    public interface IEntityRepository<T> where T:class,IEntity,new() // class yazdığımızda referans tip olabilir demiş oluyoruz. T referans tipi olmalı , IEntity ya da IEntity den türetilmiş bir class olmalı demiş olduk.Fakat IEntity yazdığımızda soyut olduğu için işimize yaramayacak. new() diyerek bunun newlendiğini söylüyoruz. İnterface'ler newlenemediği için sadece IEntity den İmplemente edilmiş somut nesneleri kullanabiliriz.
    {

        List<T> GetAll(Expression<Func<T,bool>> filter = null); // GetAll kullanırken filtre yapmamızı sağlar fakat filter=null ile filtre yazmak zorunda değiliz.Ayrı ayrı metodlar yazmamıza gerek kalmaz.
        //refactoring
        T Get(Expression<Func<T, bool>> filter);  // tek bir data getirmek için. tek bir seçeneğin detayına gitmek için kullanabiliriz.Burada filtre vermek zorunludur.
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
