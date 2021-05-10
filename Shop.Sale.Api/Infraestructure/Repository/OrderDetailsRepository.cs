using Shop.Shared.Core;
using Shop.Sale.Api.Infraestructure.Repository.Contracts;
using Shop.Sale.Api.Data.Entity;
using Shop.Sale.Api.Infraestructure.Context;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Sale.Api.Infraestructure.Repository
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
