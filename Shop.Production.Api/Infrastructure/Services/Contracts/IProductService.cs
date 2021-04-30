using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Production.Api.Infrastructure.Services.ServicesResult.Models;
using Shop.Production.Api.Infrastructure.Services.ServicesResult.Models.Product;
using Shop.Production.Api.Infrastructure.Services.ServicesResult.Core;

namespace Shop.Production.Api.Infrastructure.Services.Contracts
{
    public interface IProductService
    {
        ProductServiceResultCore GetProducts();
      ProductServiceResultCore GetProductByID(int id);
        Task<ProductServiceResultCore> SaveProduct(ProductServiceResultModel oProductServiceResultModel);
        Task<ProductServiceResultCore> UpdateProduct(ProductServiceResultModel oProductServiceResultModel);
        Task<ProductServiceResultCore> RemoveProduct(ProductServiceResultDeletedModel oProductServiceResultModel);
    }
}
