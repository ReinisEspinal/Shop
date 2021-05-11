using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Shop.Production.Api.Infrastructure.Data.Entities;
using Shop.Sale.Api.Infraestructure.Context;
using Shop.Sale.Api.Infraestructure.Repository.Contracts;
using Shop.Shared.Core;

namespace Shop.Sale.Api.Infraestructure.Repository
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
