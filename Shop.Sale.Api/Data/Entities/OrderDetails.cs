using Shop.Shared.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Sale.Api.Data.Entities
{
    [Table(name:"OrderDetails",Schema ="Sales")]
    public class OrderDetails : BaseEntity
    {
        [Key]
        [Column(Order = 1)]
        public int OrderId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short QTY { get; set; }
        public decimal Discount { get; set; }
    }
}
