using Microsoft.EntityFrameworkCore;
using Shop.Production.Api.Data.Entities;

namespace Shop.Production.Api.Infrastructure.Context
{
    public class ProductionContext : DbContext
    {
        //private string _cnnString { get; set; }
        //public ProductionContext(string cnnString)
        //{
        //    this._cnnString = cnnString;
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //     options.UseSqlServer(_cnnString);
        }

        public ProductionContext(DbContextOptions<ProductionContext> options)
          : base(options)
        {


        }

        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
    }
}
