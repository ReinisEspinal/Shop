using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Shop.Production.Api.Infrastructure.Services.Models.Category
{
    public class CategoriesDeleteModel
    {
        [Required]
        [Range(1,100)]
        public int UserDeleted { get; set; }
        [Required]
        public DateTime DeletedDate { get; }
        [Required]
        public bool Deleted { get; set; }

        public CategoriesDeleteModel()
        {
            DeletedDate = System.DateTime.Now;
            Deleted = true;
            UserDeleted = 1;
        }
    }
}
