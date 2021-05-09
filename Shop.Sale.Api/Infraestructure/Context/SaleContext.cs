using Microsoft.EntityFrameworkCore;
using Shop.Sale.Api.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infraestructure.Context
{
    public class SaleContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

        }

        public SaleContext(DbContextOptions<SaleContext> options) : base(options)
        { 
        }

        public virtual DbSet<Shipper> Sales { get; set;}
    }
}
