using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Production.Api.Infrastructure.Services.ServicesResult.Models.Product
{
    public class ProductServiceResultGetModel
    {
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set;}
        public bool Discontinued { get; set;}

        //Propiedades de otras entidades
        public string CompanyName { get; set; }
    }
}
