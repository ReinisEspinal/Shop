using Shop.Sale.Api.Infrastructure.Services.Core;
using Shop.Sale.Api.Infrastructure.Services.Models.Customers;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infrastructure.Services.Contracts
{
    public interface ICustomersServices
    {
        CustomersServicesResponse GetCustomers();
        Task<CustomersServicesResponse> SaveCustomer(CustomersAddModel oCustomerServicesResultModel);
        Task<CustomersServicesResponse> UpdateCustomer(CustomersModifyModel oCustomerServicesResultModifyModel);
        Task<CustomersServicesResponse> RemoveCustomer(int id);
        Task<CustomersServicesResponse> GetCustomerById(int id);
        Task<bool> ValidateProduct(string customerName);
    }
}
