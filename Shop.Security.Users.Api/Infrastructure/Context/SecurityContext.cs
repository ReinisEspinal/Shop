using Microsoft.EntityFrameworkCore;
using Shop.Security.Api.Infrastructure.Data;

namespace Shop.Security.Api.Infrastructure.Context
{
    public class SecurityContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
        }
        public SecurityContext(DbContextOptions<SecurityContext> options) 
            : base(options)
        {

        }
        public virtual DbSet<User> Users { get; set; }
    }
}
