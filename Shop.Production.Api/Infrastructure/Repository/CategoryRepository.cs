using Shop.Production.Api.Infrastructure.Context;
using Shop.Production.Api.Infrastructure.Data.Entities;
using Shop.Production.Api.Infrastructure.Repository.Contracts;
using Shop.Shared.Core;

namespace Shop.Production.Api.Infrastructure.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly ProductionContext _Db;
        public CategoryRepository(ProductionContext db) : base(db)
        {
        }
    }
}
