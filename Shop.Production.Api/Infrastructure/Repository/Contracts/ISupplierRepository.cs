using Shop.Shared.Contracts;
using Shop.Production.Api.Infrastructure.Data.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Shop.Production.Api.Infrastructure.Repository.Contracts
{
    public interface ISupplierRepository : IBaseRepository<Supplier>
    {
        Task AddSupplier(Supplier oSupplier);
        void UpdateSupplier(Supplier oSupplier);
        void RemoveSupplier(Supplier oSupplier);
        Task<Supplier> GetSupplierByID(int supplierID);
        IEnumerable<Supplier> GetSuppliers();
        Task<bool> SaveSupplier();
        Task<bool> SupplierExists();

    }
}
