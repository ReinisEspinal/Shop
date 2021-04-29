using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Production.Api.Infrastructure.Services.ServiceResult.Models
{
    public class ProductServiceResultModels
    {
        public string ProductName { get; set; }
        /// <summary>
        /// Nombre del suplidor
        /// </summary>
        public string CompanyName { get; set; }
        // public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Discontinued { get; set; }
    }
}
