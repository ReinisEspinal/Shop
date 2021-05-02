using Shop.Shared.Core;
using Shop.Production.Api.Infrastructure.Data.Entities;
using Shop.Production.Api.Infrastructure.Repository.Contracts;
using System;
using System.Threading.Tasks;
using Shop.Production.Api.Infrastructure.Context;
using System.Collections.Generic;

namespace Shop.Production.Api.Infrastructure.Repository
{
    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(ProductionContext db) : base(db)
        {
        }


    }
}
