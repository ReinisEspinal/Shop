using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infraestructure.Service.Models.Customers
{
    public class CustomersDeletedModel
    {
        [Required]
        [Range(1, 100)]
        public int UserDeleted { get; set; }
        [Required]
        public DateTime DeletedDate { get; }
        [Required]
        public bool Deleted { get; set; }

        public CustomersDeletedModel()
        {
            DeletedDate = System.DateTime.Now;
            Deleted = true;
            UserDeleted = 1;
        }
    }
}
