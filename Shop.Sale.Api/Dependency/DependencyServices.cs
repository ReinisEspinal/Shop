using Microsoft.Extensions.DependencyInjection;
using Shop.Sale.Api.Infraestructure.Repository.Contracts;
using Shop.Sale.Api.Infraestructure.Service.Contracts;
using Shop.Sale.Api.Infraestructure.Repository;
using Shop.Sale.Api.Infraestructure.Service;
using Shop.Production.Api.Infrastructure.Services;

namespace Shop.Sale.Api.Dependency
{
    public class DependencyServices
    {
        public static void InitializeApplicationDependencies(IServiceCollection services)
        {
            services.AddScoped<IShippersRepository, ShippersRepository>();
            services.AddTransient<IShippersService, ShippersService>();

            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddTransient<ICustomersService, CustomersService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();
        }
    }
}
