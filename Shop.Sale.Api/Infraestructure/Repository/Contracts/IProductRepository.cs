using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Shared.Contracts;
using Shop.Production.Api.Infrastructure.Data.Entities;
using System.Linq.Expressions;

namespace Shop.Sale.Api.Infraestructure.Repository.Contracts
{
    public interface IProductRepository : IBaseRepository<Product>
    {
    }
}
