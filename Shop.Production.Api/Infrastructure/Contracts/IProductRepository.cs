using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Shared.Contracts;
using Shop.Production.Api.Infrastructure.Entities;
using System.Linq.Expressions;

namespace Shop.Production.Api.Infrastructure.Contracts
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task AddProduct(Product product);
        void UpdateProduct(Product product);
        void RemoveProduct(Product product);
        IEnumerable<Product> GetProducts();
        Task<Product> GetProductByID(int productId);
        Task<bool> SaveProduct();
        Task<bool> ProductExists ();
    }
}
