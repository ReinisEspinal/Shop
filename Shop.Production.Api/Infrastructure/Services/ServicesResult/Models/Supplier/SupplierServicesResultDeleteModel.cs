using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Production.Api.Infrastructure.Services.ServicesResult.Models.Supplier
{
    public class SupplierServicesResultDeleteModel
    {
        public int UserDeleted { get; set; }
        public DateTime DeletedDate { get; }
        public bool Deleted { get; set; }

        public SupplierServicesResultDeleteModel()
        {
            DeletedDate = System.DateTime.Now;
            Deleted = true;
        }
    }
}
