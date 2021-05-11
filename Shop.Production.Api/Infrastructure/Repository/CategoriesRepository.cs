using Shop.Production.Api.Data.Entities;
using Shop.Production.Api.Infrastructure.Context;
using Shop.Production.Api.Infrastructure.Repository.Contracts;
using Shop.Shared.Core;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Production.Api.Infrastructure.Repository
{
    public class CategoriesRepository : BaseRepository<Categories>, ICategoriesRepository
    {

        public CategoriesRepository(ProductionContext db) : base(db)
        {
        }
        public override IEnumerable<Categories> FindAll()
        {
            return base.FindAll().Where(c => c.Deleted == false);

        }
    }
}
