using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Production.Api.Infrastructure.Services.ServicesResult.Models.Category
{
    public class CategoryServiceResultDeleteModel
    {
        public int UserDeleted { get; set; }
        public DateTime DeletedDate { get; }
        public bool Deleted { get; set; }

        public CategoryServiceResultDeleteModel()
        {
            DeletedDate = System.DateTime.Now;
            Deleted = true;
            UserDeleted = 1;
        }
    }
}
