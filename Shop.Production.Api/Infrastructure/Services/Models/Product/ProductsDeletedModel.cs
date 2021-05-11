using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Production.Api.Infrastructure.Services.Models.Product
{
    public class ProductsDeletedModel
    {
        public int ProductId { get; set; }
        public bool Deleted { get; set; }
        public int UserDeleted { get; set; }
        public DateTime DeletedDate { get; set; }

        public ProductsDeletedModel()
        {
            this.DeletedDate = System.DateTime.Now;
        }

    }
}
