using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //Context : Db tabloları ile proje classlarını bağlamak
    public class NorthwindContext:DbContext
    {
        //bu metod projenin hangi veritabanıyla ilişkilendirileceğini belirttiğimiz yerdir.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //  c# da \ kullandığımız yerlerde başında @ kullan.
            //Sql Server kullanacağımızı ve bu sql server 'a nasıl bağlanacağımızı belirttik.
            //Gerçek projede @"Server=175.45.2.12" bu şekilde yazılır.
            //Sql case insensitive'dir
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb ; Database=Northwind ; Trusted_Connection = true");
        }


        //veritabanındaki hangi kolon hangi nesneye denk gelicek bunları belirtiyoruz. 
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
