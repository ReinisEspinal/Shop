using Microsoft.EntityFrameworkCore;
using Shop.Sale.Api.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infrastructure.Context
{
    public class SaleContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

        }

        public SaleContext(DbContextOptions<SaleContext> options) : base(options)
        {
        }

        public virtual DbSet<Shippers> Shippers { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>().HasKey(OrderDetails => new { OrderDetails.OrderId, OrderDetails.ProductId });
        }


        //     modelBuilder.Entity<Business_attrib2object>().HasKey(ba => new { ba.IdClass, ba.IdAttribute });      

    }
}
