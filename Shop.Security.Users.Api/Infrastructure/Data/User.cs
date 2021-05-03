using Shop.Shared.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Security.Api.Infrastructure.Data
{
    [Table("Users", Schema="SECURITY")]
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
