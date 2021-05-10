using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infraestructure.Service.Models.Product
{
    public class ProductGetModel
    {

        public int ProductId { get; set; }

        public string ProductName { get; set; }
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        public bool Discontinued { get; set; }

        //Propiedades de otras entidades

        public string CompanyName { get; set; }

        public string CategoryName { get; set; }
    }
}
