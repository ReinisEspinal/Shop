using System.ComponentModel.DataAnnotations.Schema;
using Shop.Production.Api.Data.Entities;
using Shop.Shared.Core;
using System.ComponentModel.DataAnnotations;
namespace Shop.Production.Api.Data.Entities
{
    [Table("Products", Schema = "Production")]
    public class Products : BaseEntity
    {
        [Key]
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Discontinued { get; set; }
        public virtual Categories Categories { get; set; }
        public virtual Suppliers Suppliers { get; set; }
    }
}
