using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infrastructure.Services.Models.Shippers
{
    public class ShippersEditModel
    {
        public int ShipperId { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public int UserMod { get; set; }
        public DateTime ModifyDate { get; }

        public ShippersEditModel()
        {
            ModifyDate= DateTime.Now;
        }
    }
}
