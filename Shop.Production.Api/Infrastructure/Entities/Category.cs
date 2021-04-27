using System;
using System.Collections.Generic;
using Shop.Shared.Core;


namespace Shop.Production.Api.Infrastructure.Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Categoryid { get; set; }
        public string Categoryname { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
