using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Production.Api.Infrastructure.Services.ServicesResult.Models.Supplier
{
    public class SupplierServiceResultModifyModel
    {
        public int SupplierId { get; set; }
        public string CompanyName { get; set; }
        public string contactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public int UserMod { get; set; }
        public DateTime ModifyDate { get; }
        public SupplierServiceResultModifyModel()
        {
            ModifyDate = System.DateTime.Now;
        }
    }
}
