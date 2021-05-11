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


    }
}