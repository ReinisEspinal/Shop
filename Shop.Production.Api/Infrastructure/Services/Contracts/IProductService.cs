using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Production.Api.Infrastructure.Services.ServicesResult.Core;

namespace Shop.Production.Api.Infrastructure.Services.Contracts
{
    public interface IProductService
    {
        ProductServiceResultCore GetProducts();
        Task<ProductServiceResultCore> GetProductByID();
        Task<ProductServiceResultCore> SaveProduct();
        Task<ProductServiceResultCore> UpdateProduct();
        Task<ProductServiceResultCore> DeleteProduct();
    }
}
