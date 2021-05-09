using System.ComponentModel.DataAnnotations.Schema;
using Shop.Shared.Core;

namespace Shop.Sale.Api.Data.Entity
{
    [Table(name: "Shippers", Schema = "Sales")]
    public class Shipper : BaseEntity
    {

        public int ShipperId { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
    }
}
