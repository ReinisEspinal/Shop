using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infrastructure.Services.Models.OrderDetails
{
    public class OrderDetailsDeleteModel
    {
        public int OrderId { get; set;}
        public int ProductId { get; set; }
        public int UserDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public bool Deleted { get; set;}

        public OrderDetailsDeleteModel()
        {
            this.DeletedDate = System.DateTime.Now;
            this.Deleted = true;
        }
    }
}
