using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shop.Production.Api.Infrastructure.Repository.Contracts;
using Shop.Production.Api.Infrastructure.Services.Contracts;
using Shop.Production.Api.Infrastructure.Services.ServicesResult.Core;
using Shop.Production.Api.Infrastructure.Services.ServicesResult.Models;
using System.Linq;

namespace Shop.Production.Api.Infrastructure.Services
{
    public class SupplierService : ISupplierService
    {
        public readonly ISupplierRepository _SupplierRepository;
        public readonly IProductRepository _ProductRepository;
        public readonly ILogger<SupplierService> _logger;
        private readonly IConfiguration _Configuration;
        public SupplierService(ISupplierRepository supplierRepository,
            IProductRepository productRepository,
                               ILogger<SupplierService> logger,
                              IConfiguration configuration)
        {
            this._SupplierRepository = supplierRepository;
            this._ProductRepository = productRepository;
            this._Configuration = configuration;
            this._logger = logger;
        }

        public SupplierServiceResultCore GetSupplier()
        {
            SupplierServiceResultCore supplieResult = new SupplierServiceResultCore();

            try
            {
                var suppliers = _SupplierRepository.FindAll(oSupplier => !oSupplier.Deleted);
                var products = _ProductRepository.FindAll(oProduct => !oProduct.Deleted);

                var query = (from supplier in suppliers
                             join product in products
                             on supplier.SupplierId equals product.SupplierId
                             select new SupplierServiceResultModel
                             {
                                 CompanyName = supplier.CompanyName,
                                 ContactName = supplier.ContactName,
                                 Address = supplier.Address
                             }).ToList();
                supplieResult.Data = query;
                supplieResult.Message = "Lista de suplidores";
                supplieResult.Success = true;
            }
            catch (System.Exception e)
            {

                _logger.LogError($"Error {e.Message}");
            }

            return supplieResult;
        }
    }
}
