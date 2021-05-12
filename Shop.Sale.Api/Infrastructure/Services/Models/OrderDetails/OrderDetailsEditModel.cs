using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infrastructure.Services.Models.OrderDetails
{
    public class OrderDetailsEditModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short QTY { get; set; }
        public decimal Discount { get; set; }
        public int UserMod {get;set;}
        public DateTime ModifyDate { get; set; }

        public OrderDetailsEditModel()
        {
            this.ModifyDate = System.DateTime.Now;
        }
    }
}
