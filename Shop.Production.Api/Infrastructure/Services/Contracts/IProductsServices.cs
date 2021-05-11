using System.Threading.Tasks;
using Shop.Production.Api.Infrastructure.Services.Core;
using Shop.Production.Api.Infrastructure.Services.Models.Product;

namespace Shop.Production.Api.Infrastructure.Services.Contracts
{
    public interface IProductsServices
    {
        ProductsServicesResponse GetProducts();
        Task<ProductsServicesResponse> SaveProduct(ProductsAddModel oProductServiceResultModel);
        Task<ProductsServicesResponse> UpdateProduct(ProductsModifyModel oProductServiceResultModel);
        Task<ProductsServicesResponse> RemoveProduct(int id);
        Task<ProductsServicesResponse> GetProductById(int id);
        Task<bool> ValidateProduct(string productName);
    }
}
