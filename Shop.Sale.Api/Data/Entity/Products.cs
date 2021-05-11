using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shop.Shared.Core; 

namespace Shop.Production.Api.Infrastructure.Data.Entities
{
    [Table("Products", Schema = "Production")]
    public class Products : BaseEntity
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
