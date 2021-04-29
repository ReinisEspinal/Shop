using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Shop.Production.Api.Infrastructure.Context;
using Shop.Production.Api.Infrastructure.Repository.Contracts;
using Shop.Production.Api.Infrastructure.Data.Entities;
using Shop.Shared.Core;

namespace Shop.Production.Api.Infrastructure.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly ProductionContext _db;
        public ProductRepository(ProductionContext db) : base(db)
        {
            this._db = db;
        }

        public async Task AddProduct(Product product)
        {
            try
            {
                await base.Add(product);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<Product> GetProductByID(int productId)
        {
            try
            {
                return await base.GetById(productId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            try
            {
                return base.FindAll(oProduct => !oProduct.Deleted);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> ProductExists()
        {
            try
            {
                return await base.Exists(oProducto => !oProducto.Deleted);

            }
            catch (Exception e)
            {

                throw new Exception (e.Message);
            }
        }

        public void RemoveProduct(Product product)
        {
            try
            {
                base.Remove(product);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<bool> SaveProduct()
        {
            try
            {
                return base.Commit();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                base.Update(product);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
