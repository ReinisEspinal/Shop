using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Shop.Production.Api.Infrastructure.Context;
using Shop.Production.Api.Infrastructure.Repository.Contracts;
using Shop.Production.Api.Data.Entities;
using Shop.Shared.Core;

namespace Shop.Production.Api.Infrastructure.Repository
{
    public class ProductsRepository : BaseRepository<Products>, IProductsRepository
    {
        public ProductsRepository(ProductionContext db) : base(db)
        {

        }

        public override IEnumerable<Products> FindAll()
        {
  
            return base.FindAll().Where(product => !product.Deleted);

        }
     
    }
}
