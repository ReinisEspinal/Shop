using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infrastructure.Services.Models.Shippers
{
    public class ShippersDeleteModel
    {
        public int ShipperId { get; set; }
        public int UserDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public bool Deleted { get; set; }
        public ShippersDeleteModel()
        {
            DeletedDate = System.DateTime.Now;
            Deleted = true;
        }
    }
}
