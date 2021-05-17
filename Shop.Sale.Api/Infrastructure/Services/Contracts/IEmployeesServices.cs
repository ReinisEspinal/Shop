using Shop.Sale.Api.Infrastructure.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infrastructure.Services.Contracts
{
    public interface IEmployeesServices
    {
        EmployeesServicesResponse GetEmployees();
    }
}
