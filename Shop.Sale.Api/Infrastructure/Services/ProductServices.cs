using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shop.Sale.Api.Infrastructure.Repository.Contracts;
using Shop.Sale.Api.Infrastructure.Services.Contracts;

namespace Shop.Production.Api.Infrastructure.Services
{
    public class ProductServices : IProductServices
    {
        public readonly IProductRepository _ProductRepository;
        public readonly ILogger<ProductServices> _logger;
        private readonly IConfiguration _Configuration;

        public ProductServices(IProductRepository productRepository,
                               ILogger<ProductServices> logger,
                              IConfiguration configuration)
        {
            this._ProductRepository = productRepository;
            this._Configuration = configuration;
            this._logger = logger;
        }


    }
}