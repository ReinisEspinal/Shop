using Shop.Sale.Api.Infraestructure.Service.Core;
using Shop.Sale.Api.Infraestructure.Service.Models.Customers;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infraestructure.Service.Contracts
{
    public interface ICustomersService
    {
        CustomersServiceResponse GetCustomers();
        Task<CustomersServiceResponse> SaveCustomer(CustomersAddModel oCustomerServiceResultModel);
        Task<CustomersServiceResponse> UpdateCustomer(CustomersModifyModel oCustomerServiceResultModifyModel);
        Task<CustomersServiceResponse> RemoveCustomer(int id);
        Task<CustomersServiceResponse> GetCustomerById(int id);
        Task<bool> ValidateProduct(string customerName);
    }
}
