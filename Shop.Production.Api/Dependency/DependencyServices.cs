using Microsoft.Extensions.DependencyInjection;
using Shop.Production.Api.Infrastructure.Repository.Contracts;
using Shop.Production.Api.Infrastructure.Services.Contracts;
using Shop.Production.Api.Infrastructure.Repository;
using Shop.Production.Api.Infrastructure.Services;
namespace Shop.Production.Api.Dependency
{
    public class DependencyServices
    {
        public static void InitializeApplicationDependencies(IServiceCollection services)
        {
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddTransient<IProductsServices, ProductsServices>();

            services.AddScoped<ISuppliersRepository, SuppliersRepository>();
            services.AddTransient<ISuppliersServices, SuppliersServices>();

            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddTransient<ICategoriesServices, CategoriesServices>();
        }
    }
}
