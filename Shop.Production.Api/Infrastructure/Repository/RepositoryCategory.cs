using Shop.Production.Api.Infrastructure.Context;
using Shop.Production.Api.Infrastructure.Contracts;
using Shop.Production.Api.Infrastructure.Entities;
using Shop.Shared.Core;
namespace Shop.Production.Api.Infrastructure.Repository
{
    public class RepositoryCategory : BaseRepository<Category>, ICategoryRepository
    {
        public RepositoryCategory(ProductionContext context) : base(context)
        {

        }

    }
}
