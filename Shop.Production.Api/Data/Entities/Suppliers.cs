using Shop.Shared.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.Production.Api.Data.Entities
{
    [Table("Suppliers", Schema = "Production")]
    public class Suppliers : BaseEntity
    {
        public Suppliers()
        {
            Products = new HashSet<Products>();
        }
        [Key]
        public int SupplierId { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public virtual ICollection<Products> Products { get; set; }
    }
}
