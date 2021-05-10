using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Sale.Api.Infraestructure.Service.Models.Product
{
    public class ProductAddModel
    {
        [StringLength(20)]
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Product Name  is mandatory")]
        public string ProductName { get; set; }

        [Required]
        [Range(1, 300)]
        //Validar que el id del suplidor esta en la base de datos sin estar eliminado
        public int SupplierId { get; set; }

        [Required]
        [Range(1, 300)]
        //Validar que el id de la categoria esta en la base de datos sin estar eliminado
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



        public ProductAddModel()
        {
            CreationDate = System.DateTime.Now;
        }

    }

}
