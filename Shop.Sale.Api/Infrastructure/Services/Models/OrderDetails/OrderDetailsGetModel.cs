using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infrastructure.Services.Models.Orders
{
    public class OrderDetailsGetModel
    {
        public int OrderId { get; set; }
        public decimal UnitPrice { get; set; }
        public short QTY { get; set; }
        public decimal Discount { get; set; }

        //Propiedades de otras entidades
        public string ProductName { get; set; }
        public string EmployeeName { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
