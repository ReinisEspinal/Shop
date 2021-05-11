using System;
using System.ComponentModel.DataAnnotations.Schema;
using Shop.Shared.Core; 

namespace Shop.Production.Api.Infrastructure.Data.Entities
{
    [Table("Products", Schema = "Production")]
    public class Products : BaseEntity
    {
        public string ProductName { get; set; }
    }
}
