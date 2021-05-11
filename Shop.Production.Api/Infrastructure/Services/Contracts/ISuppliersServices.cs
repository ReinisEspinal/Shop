using System.Threading.Tasks;
using Shop.Production.Api.Infrastructure.Services.Core;
using Shop.Production.Api.Infrastructure.Services.Models.Supplier;

namespace Shop.Production.Api.Infrastructure.Services.Contracts
{
    public interface ISuppliersServices
    {
        Task<SuppliersServicesResponse> SaveSupplier(SuppliersAddModel oSupplier);
        SuppliersServicesResponse GetSuppliers();
        Task<SuppliersServicesResponse> GetSupplierById(int id);
        Task<SuppliersServicesResponse> UpdateSupplier(SuppliersModifyModel oSupplier);
        Task<SuppliersServicesResponse> DeleteSupplier(int id);
    }
}
