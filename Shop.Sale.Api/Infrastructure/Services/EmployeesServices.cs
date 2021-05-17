
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shop.Sale.Api.Infrastructure.Repository.Contracts;
using Shop.Sale.Api.Infrastructure.Services.Contracts;
using Shop.Sale.Api.Infrastructure.Services.Core;
using System;

namespace Shop.Sale.Api.Infrastructure.Services
{
    public class EmployeesServices : IEmployeesServices
    {
        private readonly IEmployeesRepository _IEmployeesRepository;
        private readonly ILogger<EmployeesServices> _ILogger;
        private readonly IConfiguration _IConfiguration;

        public EmployeesServices(IEmployeesRepository iEmployeesRepository,
            ILogger<EmployeesServices> iLogger,
            IConfiguration iConfiguration)
        {
            this._IEmployeesRepository = iEmployeesRepository;
            this._ILogger = iLogger;
            this._IConfiguration = iConfiguration;
        }

        public EmployeesServicesResponse GetEmployees()
        {
            var servicesResult = new EmployeesServicesResponse();
            try
            {
                servicesResult.Data = _IEmployeesRepository.FindAll();
            }
            catch (Exception e)
            {
                _ILogger.LogError($"{e.Message}");
                servicesResult.Message = "Error obteniendo los clientes";
                servicesResult.Success = false;
            }
            return servicesResult;
        }

    }
}
