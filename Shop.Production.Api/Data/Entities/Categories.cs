using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Shop.Production.Api.Data.Entities;
using Shop.Shared.Core;
using System.ComponentModel.DataAnnotations;

namespace Shop.Production.Api.Data.Entities
{
    [Table("Categories", Schema = "Production")]
    public class Categories : BaseEntity
    {
        public Categories()
        {
            Products = new HashSet<Products>();
        }
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
