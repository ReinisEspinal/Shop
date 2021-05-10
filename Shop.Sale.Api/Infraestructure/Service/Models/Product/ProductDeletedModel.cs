using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infraestructure.Service.Models.Product
{
    public class ProductDeletedModel
    {
        public int ProductId { get; set; }
        public bool Deleted { get; set; }
        public int UserDeleted { get; set; }
        public DateTime DeletedDate { get; set; }

        public ProductDeletedModel()
        {
            this.DeletedDate = System.DateTime.Now;
        }

    }
}
