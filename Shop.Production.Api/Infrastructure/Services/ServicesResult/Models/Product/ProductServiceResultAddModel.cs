using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Production.Api.Infrastructure.Services.ServicesResult.Models.Product
{
    public class ProductServiceResultAddModel
    {
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Discontinued { get; set; }
        public int CreationUser { get; set;}
        public DateTime CreationDate { get; }


        public ProductServiceResultAddModel()
        {
            CreationDate = System.DateTime.Now;
        }

    }
}
