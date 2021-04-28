using Shop.Shared.Contracts;
using Shop.Production.Api.Infrastructure.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Shop.Production.Api.Infrastructure.Contracts
{
    public interface ISupplierRepository : IBaseRepository<Supplier>
    {
        Task AddSupplier(Supplier oSupplier);
       void Update(Supplier oSupplier);
        void Remove(Supplier oSupplier);
        Task<Supplier> GetSupplierByID(int supplierID);
        IEnumerable<Supplier> GetSuppliers();
        Task<bool> SaveSupplier();
        Task<bool> SupplierExists();

    }
}
