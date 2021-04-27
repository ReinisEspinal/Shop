using System;
using Shop.Shared.Core; 

namespace Shop.Production.Api.Infrastructure.Entities
{
    public class Product : BaseEntity
    {
        public int Productid { get; set; }
        public string Productname { get; set; }
        public int Supplierid { get; set; }
        public int Categoryid { get; set; }
        public decimal Unitprice { get; set; }
        public bool Discontinued { get; set; }
        public virtual Category Categories { get; set; }
        public virtual Supplier Suppliers { get; set; }
    }
}
