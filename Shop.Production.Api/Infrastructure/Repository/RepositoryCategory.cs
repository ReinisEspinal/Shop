using Shop.Production.Api.Infrastructure.Context;
using Shop.Production.Api.Infrastructure.Repository.Contracts;
using Shop.Production.Api.Infrastructure.Data.Entities;
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
