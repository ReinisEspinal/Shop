using Microsoft.EntityFrameworkCore;
using Shop.Production.Api.Infrastructure.Entities;

namespace Shop.Production.Api.Infrastructure.Context
{
    public class ProductionContext : DbContext
    {
        public ProductionContext(DbContextOptions<ProductionContext> options)
          : base(options)
        {

        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
    }
}
