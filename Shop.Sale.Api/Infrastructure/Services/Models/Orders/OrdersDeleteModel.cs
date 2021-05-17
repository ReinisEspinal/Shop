using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infrastructure.Services.Models.Orders
{
    public class OrdersDeleteModel
    {
        public int OrderId { get; set; }
        public int UserDeleted { get; set; }
        public bool Deleted { get; set; }
        public DateTime DeletedDate { get; set; }

        public OrdersDeleteModel()
        {
            DeletedDate = System.DateTime.Now;
            Deleted = true;
        }
    }
}
