using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infrastructure.Services.Models.Shippers
{
    public class ShippersAddModel
    {
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public int CreateUser { get; set; }
        public DateTime CreationDate { get;}

        public ShippersAddModel()
        {
            CreationDate = DateTime.Now;
        }
    }
}
