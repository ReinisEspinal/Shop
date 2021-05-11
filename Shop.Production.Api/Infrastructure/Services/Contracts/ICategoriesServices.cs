using Shop.Production.Api.Infrastructure.Services.Core;
using Shop.Production.Api.Infrastructure.Services.Models.Category;
using System.Threading.Tasks;

namespace Shop.Production.Api.Infrastructure.Services.Contracts
{
    public interface ICategoriesServices
    {
        CategoriesServicesResponse GetCategories();
        Task<CategoriesServicesResponse> GetCategoryById(int id);
        Task<CategoriesServicesResponse> SaveCategory(CategoriesAddModel category);
        Task<CategoriesServicesResponse> UpdateCategory(CategoriesModifyModel category);
        Task<CategoriesServicesResponse> DeleteCategory(int id);
    }
}
