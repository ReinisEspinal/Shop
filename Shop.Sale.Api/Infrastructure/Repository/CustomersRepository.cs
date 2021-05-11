using Shop.Shared.Core;
using Shop.Sale.Api.Data.Entities;
using Shop.Sale.Api.Infrastructure.Repository.Contracts;
using Shop.Sale.Api.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Sale.Api.Infrastructure.Repository
{
    public class CustomersRepository : BaseRepository<Customers>, ICustomersRepository
    {
        public CustomersRepository(SaleContext db) : base(db)
        {

        }


        public override IEnumerable<Customers> FindAll()
        {
            return base.FindAll().Where(customer => !customer.Deleted);
        }
    }
}
