using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infrastructure.Services.Models.Orders
{
    public class OrdersAddModel
    {
        [Required]
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public int ShipperId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public int CreationUser { get; set; }
        public DateTime CreationDate { get; set; }
        public OrdersAddModel()
        {
            this.CreationDate = System.DateTime.Now;
        }
    }
}
