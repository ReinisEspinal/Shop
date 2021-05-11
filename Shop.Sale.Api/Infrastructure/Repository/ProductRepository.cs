using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Shop.Sale.Api.Data.Entities;
using Shop.Sale.Api.Infrastructure.Context;
using Shop.Sale.Api.Infrastructure.Repository.Contracts;
using Shop.Shared.Core;

namespace Shop.Sale.Api.Infrastructure.Repository
{
    public class ProductRepository : BaseRepository<Products>, IProductRepository
    {
        public ProductRepository(SaleContext db) : base(db)
        {

        }

        public override IEnumerable<Products> FindAll()
        {
  
            return base.FindAll().Where(product => !product.Deleted);

        }
     
    }
}
