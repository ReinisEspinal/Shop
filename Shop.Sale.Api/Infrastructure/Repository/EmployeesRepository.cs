using Shop.Sale.Api.Data.Entities;
using Shop.Sale.Api.Infrastructure.Context;
using Shop.Sale.Api.Infrastructure.Repository.Contracts;
using Shop.Shared.Contracts;
using Shop.Shared.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infrastructure.Repository
{
    public class EmployeesRepository : BaseRepository<Employees>, IEmployeesRepository
    {
        public EmployeesRepository(SaleContext db) : base(db)
        {

        }

        public override IEnumerable<Employees> FindAll()
        {
            return base.FindAll().Where(employees => !employees.Deleted);
        }
    }
}
