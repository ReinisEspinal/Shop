using Shop.Shared.Core;
using Shop.Production.Api.Infrastructure.Data.Entities;
using Shop.Production.Api.Infrastructure.Repository.Contracts;
using System;
using System.Threading.Tasks;
using Shop.Production.Api.Infrastructure.Context;
using System.Collections.Generic;

namespace Shop.Production.Api.Infrastructure.Repository
{
    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        private readonly ProductionContext _db;

        public SupplierRepository(ProductionContext db) : base(db)
        {
            this._db = db;
        }
        public async Task AddSupplier(Supplier supplier)
        {
            try
            {
                await base.Add(supplier);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void UpdateSupplier(Supplier supplier)
        {
            try
            {
                base.Update(supplier);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public void RemoveSupplier(Supplier supplier)
        {
            try
            {
                base.Remove(supplier);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<Supplier> GetSupplierByID(int supplierID)
        {
            try
            {
                return await base.GetById(supplierID);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<Supplier> GetSuppliers()
        {
            try
            {
                var result = base.FindAll(oSupplier => !oSupplier.Deleted);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public async Task<bool> SaveSupplier()
        {
            try
            {
                return await base.Commit();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> SupplierExists()
        {
            try
            {
                return await base.Exists(oSupplier => !oSupplier.Deleted);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }


    }
}
