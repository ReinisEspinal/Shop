using Shop.Shared.Core;
using Shop.Sale.Api.Infrastructure.Repository.Contracts;
using Shop.Sale.Api.Data.Entities;
using Shop.Sale.Api.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Sale.Api.Infrastructure.Repository
{
    public class OrderDetailsRepository : BaseRepository<OrderDetails>, IOrderDetailsRepository
    {
        public OrderDetailsRepository(SaleContext db) : base(db)
        {

        }

        public override IEnumerable<OrderDetails> FindAll()
        {
            return base.FindAll().Where(OrderDetails => !OrderDetails.Deleted);
        }

    }
}
