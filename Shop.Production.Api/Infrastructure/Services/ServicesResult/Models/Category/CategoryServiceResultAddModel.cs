using System;

namespace Shop.Production.Api.Infrastructure.Services.ServicesResult.Models.Category
{
    public class CategoryServiceResultAddModel 
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int CreationUser { get; set; }
        public DateTime CreationDate { get; set; }

        public CategoryServiceResultAddModel()
        {
            CreationDate = System.DateTime.Now;
        }
    }
}
