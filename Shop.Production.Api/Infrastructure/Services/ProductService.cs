using Shop.Production.Api.Infrastructure.Services.Contracts;
using Shop.Production.Api.Infrastructure.Repository.Contracts;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Shop.Production.Api.Infrastructure.Services.ServicesResult.Core;
using System;
using System.Linq;
using Shop.Production.Api.Infrastructure.Services.ServiceResult.Models;
using Microsoft.Extensions.Logging;

namespace Shop.Production.Api.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        public readonly IProductRepository _ProductRepository;
        public readonly ISupplierRepository _SupplierRepository;

        public readonly ILogger<ProductService> _logger;
        private readonly IConfiguration _Configuration;


        public ProductService(IProductRepository productRepository,
                              ISupplierRepository supplierRepository,

                               ILogger<ProductService> logger,
                              IConfiguration configuration)
        {
            this._ProductRepository = productRepository;
            this._SupplierRepository = supplierRepository;
            this._Configuration = configuration;
            this._logger = logger;
        }

        public Task<ProductServiceResultCore> DeleteProduct()
        {
            throw new System.NotImplementedException();
        }

        public Task<ProductServiceResultCore> GetProductByID()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Metodo para obtener la lista de producto. Falta obtener el dato de categoria; hay que realizar el repositorio.
        /// </summary>
        /// <returns></returns>
        public ProductServiceResultCore GetProducts()
        {
            ProductServiceResultCore productResult = new ProductServiceResultCore();
            try
            {
                var products = _ProductRepository.FindAll(pProducts => !pProducts.Deleted);
                var suppliers = _SupplierRepository.FindAll(sSupplier => !sSupplier.Deleted);

                var query = (from product in _ProductRepository.GetProducts()
                             join supplier in _SupplierRepository.GetSuppliers()
                             on product.Supplierid equals supplier.Supplierid
                             select new ProductServiceResultModels
                             {
                                 ProductName = product.Productname,
                                 CompanyName = supplier.Companyname,
                                 UnitPrice = product.Unitprice,
                                 Discontinued = product.Discontinued
                             }).ToList();

                productResult.Data = query;
                productResult.Message = "Lista de productos";
                productResult.Success = true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error {e.Message}");
                productResult.Success = false;
                productResult.Message = "Error obteniendo los productos";
            }

            return productResult;
        }

        public Task<ProductServiceResultCore> SaveProduct()
        {
            throw new System.NotImplementedException();
        }

        public Task<ProductServiceResultCore> UpdateProduct()
        {
            throw new System.NotImplementedException();
        }
    }
}