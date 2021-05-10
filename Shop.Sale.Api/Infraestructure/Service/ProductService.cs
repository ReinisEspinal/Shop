using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Shop.Production.Api.Infrastructure.Data.Entities;
using Shop.Sale.Api.Infraestructure.Repository.Contracts;
using Shop.Sale.Api.Infraestructure.Service.Contracts;
using Shop.Sale.Api.Infraestructure.Service.Core;
using Shop.Sale.Api.Infraestructure.Service.Models.Product;

namespace Shop.Production.Api.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        public readonly IProductRepository _ProductRepository;
        public readonly ILogger<ProductService> _logger;
        private readonly IConfiguration _Configuration;

        public ProductService(IProductRepository productRepository,
                               ILogger<ProductService> logger,
                              IConfiguration configuration)
        {
            this._ProductRepository = productRepository;
            this._Configuration = configuration;
            this._logger = logger;
        }

      public async Task<ProductsServiceResponse> GetProductById(int id)
        {
            ProductsServiceResponse productServiceResult = new ProductsServiceResponse();
            ProductGetModel productGetModel = new ProductGetModel();
            try
            {
                var oProduct = await _ProductRepository.GetById(id);


                if (oProduct ==null || oProduct.Deleted == true)
                {
                    productServiceResult.Message = "El producto no existe";
                    productServiceResult.Data = null;
                    productServiceResult.Success = true;
                    return productServiceResult;
                }
                else
                {
                    var query = (from product in _ProductRepository.FindAll().Where(c => c.ProductId == oProduct.ProductId)
                                 select new ProductGetModel
                                 {
                                     ProductId = product.ProductId,
                                     ProductName = product.ProductName,
                                     UnitPrice = product.UnitPrice,
                                     Discontinued = product.Discontinued,
                                 });
                    productServiceResult.Data = query;
                    productServiceResult.Message = "Producto encontrado";
                    productServiceResult.Success = true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}");
                productServiceResult.Message = "Error filtrando el producto";
                productServiceResult.Success = false;
            }
            return productServiceResult;
        }

    }
}