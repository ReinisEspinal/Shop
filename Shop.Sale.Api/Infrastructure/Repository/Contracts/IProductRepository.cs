using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Shared.Contracts;
using Shop.Sale.Api.Data.Entities;
using System.Linq.Expressions;

namespace Shop.Sale.Api.Infrastructure.Repository.Contracts
{
    public interface IProductRepository : IBaseRepository<Products>
    {
    }
}
