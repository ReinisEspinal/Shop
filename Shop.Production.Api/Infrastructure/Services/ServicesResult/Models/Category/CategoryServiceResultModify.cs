using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Production.Api.Infrastructure.Services.ServicesResult.Models.Category
{
    public class CategoryServiceResultModifyModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int UserMod { get; set; }
        public DateTime ModifyDate { get; set; }

        public CategoryServiceResultModifyModel()
        {
            ModifyDate = System.DateTime.Now;
        }
    }
}
