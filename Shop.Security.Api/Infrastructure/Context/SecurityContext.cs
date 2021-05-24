using Microsoft.EntityFrameworkCore;
using Shop.Api.Data.Entity;

namespace Shop.Security.Api.Infrastructure.Context
{
    public class SecurityContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

        }

        public SecurityContext(DbContextOptions<SecurityContext> options) : base(options)
        {
        }

        public virtual DbSet<Users> Users { get; set; }
    }
}
