using Microsoft.Extensions.DependencyInjection;
using Shop.Sale.Api.Infrastructure.Repository.Contracts;
using Shop.Sale.Api.Infrastructure.Services.Contracts;
using Shop.Sale.Api.Infrastructure.Repository;
using Shop.Production.Api.Infrastructure.Services;
using Shop.Sale.Api.Infrastructure.Service;

namespace Shop.Sale.Api.Dependency
{
    public class DependencyServices
    {
        public static void InitializeApplicationDependencies(IServiceCollection services)
        {
            services.AddScoped<IShippersRepository, ShippersRepository>();
            services.AddTransient<IShippersServices, ShippersServices>();

            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddTransient<ICustomersServices, CustomersServices>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddTransient<IProductServices, ProductServices>();

            services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();
            services.AddTransient<IOrderDetailsServices, OrderDetailsServices>();
        }
    }
}
