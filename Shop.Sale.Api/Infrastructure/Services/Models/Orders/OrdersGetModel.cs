using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infrastructure.Services.Models.Orders
{
    public class OrdersGetModel
    {
        public int OrderId { get; set; }
        //Propiedad de custoemr
        public string CustomerName { get; set; }
        //Propiedad de Empleado
        public string EmployeeName { get; set; }
        //Propiedad de Shipper
        public string ShipperName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequireDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal Freight { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
    }
}
