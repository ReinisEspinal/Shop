using Shop.Security.Api.Infrastructure.Data;
using Shop.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Security.Api.Infrastructure.Repository.Contracts
{
    public interface IUserRepository : IBaseRepository<User>
    {
    }
}
