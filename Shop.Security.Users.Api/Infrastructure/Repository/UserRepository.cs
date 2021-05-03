using Shop.Security.Api.Infrastructure.Data;
using Shop.Security.Api.Infrastructure.Repository.Contracts;
using Shop.Shared.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace Shop.Security.Api.Infrastructure.Repository
{
    public class UserRepository : BaseRepository<User>,IUserRepository
    {
        public UserRepository(SecurityContext db) : base(db)
        { }
    }
}
