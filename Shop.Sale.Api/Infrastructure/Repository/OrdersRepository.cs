using Shop.Sale.Api.Data.Entities;
using Shop.Sale.Api.Infrastructure.Context;
using Shop.Sale.Api.Infrastructure.Repository.Contracts;
using Shop.Shared.Core;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Sale.Api.Infrastructure.Repository
{
    public class OrdersRepository : BaseRepository<Orders>, IOrdersRepository
    {
        public OrdersRepository(SaleContext db) : base(db)
        {

        }

        public override IEnumerable<Orders> FindAll()
        {
            return base.FindAll().Where(order => !order.Deleted);
        }
    }
}
