using Shop.Production.Api.Infrastructure.Services.ServicesResult.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Production.Api.Infrastructure.Services.ServicesResult.Models.Product
{
    public class ProductServiceResultAddModel
    {
        [StringLength(20)]
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Product Name  is mandatory")]
         //[NameExistsAttribute]
        public string ProductName { get; set; }

        [Required]
        [Range(1, 300)]
        public int SupplierId { get; set; }

        [Required]
        [Range(1, 300)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Unit Price is mandatory")]
        [Range(1, 500000)]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Discontinued is mandatory")]
        public bool Discontinued { get; set; }

        [Required]
        [Range(1, 300)]
        public int CreationUser { get; set; }

        public DateTime CreationDate { get; }



        public ProductServiceResultAddModel()
        {
            CreationDate = System.DateTime.Now;
        }

    }

}
