using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infrastructure.Services.Models.OrderDetails
{
    public class OrderDetailsAddModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short QTY { get; set; }
        public decimal Discount { get; set; }
        public int CreationUser { get; set; }
        public DateTime CreationDate { get; set; }

        public OrderDetailsAddModel()
        {
            this.CreationDate = System.DateTime.Now;
        }
    }
}
