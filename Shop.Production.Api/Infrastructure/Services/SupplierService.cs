using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shop.Production.Api.Infrastructure.Repository.Contracts;
using Shop.Production.Api.Infrastructure.Services.Contracts;
using Shop.Production.Api.Infrastructure.Services.ServicesResult.Core;

namespace Shop.Production.Api.Infrastructure.Services
{
    public class SupplierService : ISupplierService
    {
        public readonly ISupplierRepository _SupplierRepository;
        public readonly ILogger<ProductService> _logger;
        private readonly IConfiguration _Configuration;
        public SupplierService(ISupplierRepository supplierRepository,
                               ILogger<ProductService> logger,
                              IConfiguration configuration)
        {
            this._SupplierRepository = supplierRepository;
            this._Configuration = configuration;
            this._logger = logger;
        }

        public SupplierServiceResultCore GetSupplier()
        {
            throw new System.NotImplementedException();
        }
    }
}
