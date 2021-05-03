using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shop.Production.Api.Infrastructure.Data.Entities;
using Shop.Production.Api.Infrastructure.Repository.Contracts;
using Shop.Production.Api.Infrastructure.Services.Contracts;
using Shop.Production.Api.Infrastructure.Services.ServicesResult.Core;
using Shop.Production.Api.Infrastructure.Services.ServicesResult.Models.Supplier;
using System;
using System.Threading.Tasks;

namespace Shop.Production.Api.Infrastructure.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _ISupplierRepository;
        private readonly ILogger<SupplierService> _ILogger;
        private readonly IConfiguration _IConfiguration;

        public SupplierService(ISupplierRepository iSupplierRepository, ILogger<SupplierService> iLogger, IConfiguration iConfiguration)
        {
            this._ISupplierRepository = iSupplierRepository;
            this._ILogger = iLogger;
            this._IConfiguration = iConfiguration;
        }
        public SupplierServiceResultCore GetSuppliers()
        {
            SupplierServiceResultCore supplierServiceResult = new SupplierServiceResultCore();
            try
            {
                var oSupplier = _ISupplierRepository.FindAll(c => !c.Deleted);
                supplierServiceResult.Data = oSupplier;
                supplierServiceResult.Message = "Lista de suplidores.";
                supplierServiceResult.Success = true;

            }
            catch (Exception e)
            {
                _ILogger.LogError($"Error al consultar los suplidores. {e.Message}");
                supplierServiceResult.Success = false;
                supplierServiceResult.Message = "Error consultando los suplidores";
            }

            return supplierServiceResult;
        }
        public async Task<SupplierServiceResultCore> GetSupplierById(int id)
        {
            var supplierServiceResult = new SupplierServiceResultCore();
            try
            {
                supplierServiceResult.Data = await _ISupplierRepository.GetById(id);
                supplierServiceResult.Message = "Cliente filtrado por numero de Id.";
                supplierServiceResult.Success = true;
            }
            catch (Exception e)
            {
                _ILogger.LogError($"Error al filtrar al cliente por el numero de Id.{e.Message}");
                supplierServiceResult.Message = "Error al filtrar al cliente";
            }

            return supplierServiceResult;
        }

        public async Task<SupplierServiceResultCore> SaveSupplier(SupplierServiceResultAddModel supplierAddModel)
        {
            SupplierServiceResultCore supplierServicesResult = new SupplierServiceResultCore();

            try
            {
                Supplier oSupplier = new Supplier()
                {
                    CompanyName = supplierAddModel.CompanyName,
                    ContactName = supplierAddModel.ContactName,
                    ContactTitle = supplierAddModel.ContactTitle,
                    Address = supplierAddModel.Address,
                    City = supplierAddModel.City,
                    Region = supplierAddModel.Region,
                    PostalCode = supplierAddModel.PostalCode,
                    Country = supplierAddModel.Country,
                    Phone = supplierAddModel.Phone,
                    Fax = supplierAddModel.Fax,
                    CreationUser = supplierAddModel.CreationUser,
                    CreationDate = supplierAddModel.CreationDate
                };
                await _ISupplierRepository.Add(oSupplier);
                await _ISupplierRepository.Commit();

                supplierServicesResult.Success = true;
                supplierServicesResult.Message = "Supplier Agregado.";

            }
            catch (Exception e)
            {
                _ILogger.LogError($"Error agregando el suplidor.{e.Message}");
                supplierServicesResult.Message = "Error agregando el suplidor.";
                supplierServicesResult.Success = false;
            }
            return supplierServicesResult;
        }
    
        /// <summary>
        /// Porque llamar a la base de datos para editar. Porque no mandar todo simplemente desde el cliente
        /// </summary>
        /// <param name="supplierModifyModel"></param>
        /// <returns></returns>
        public async Task<SupplierServiceResultCore> UpdateSupplier(SupplierServiceResultModifyModel supplierModifyModel)
        {
            SupplierServiceResultCore supplierServiceResult = new SupplierServiceResultCore();

            try
            {
                var supplieUpdate = await _ISupplierRepository.GetById(supplierModifyModel.SupplierId);
                {
                    supplieUpdate.CompanyName = supplierModifyModel.CompanyName;
                    supplieUpdate.ContactName = supplierModifyModel.contactName;
                    supplieUpdate.ContactTitle = supplierModifyModel.ContactTitle;
                    supplieUpdate.Address = supplierModifyModel.Address;
                    supplieUpdate.City = supplierModifyModel.City;
                    supplieUpdate.Region = supplierModifyModel.Region;
                    supplieUpdate.PostalCode = supplierModifyModel.PostalCode;
                    supplieUpdate.Country = supplierModifyModel.Country;
                    supplieUpdate.Phone = supplierModifyModel.Phone;
                    supplieUpdate.Fax = supplierModifyModel.Fax;
                    supplieUpdate.UserMod = supplierModifyModel.UserMod;
                    supplieUpdate.ModifyDate = supplierModifyModel.ModifyDate;
                };

                _ISupplierRepository.Update(supplieUpdate);

                await  _ISupplierRepository.Commit();
                supplierServiceResult.Success = true;
                supplierServiceResult.Message = "Suplidor editado.";
            }
            catch (Exception e)
            {
                _ILogger.LogError($"Error editando al suplidor.{e.Message}");
                supplierServiceResult.Message = "Error editando al suplidor.";
            }
            return supplierServiceResult;
        }
        /// <summary>
        /// Elimina un suplidor. Recibir o no el objeto?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<SupplierServiceResultCore> DeleteSupplier(int id)
        {
            SupplierServiceResultCore supplierServiceResult = new SupplierServiceResultCore();
            var supplierDeleteModel = new SupplierServicesResultDeleteModel();

            try
            {
                var oSupplier = await _ISupplierRepository.Find(c => c.SupplierId == id);
                if (!oSupplier.Deleted)
                {

                    oSupplier.UserDeleted = supplierDeleteModel.UserDeleted;
                    oSupplier.DeletedDate = supplierDeleteModel.DeletedDate;
                    oSupplier.Deleted = supplierDeleteModel.Deleted;

                    _ISupplierRepository.Update(oSupplier);

                    await _ISupplierRepository.Commit();

                    supplierServiceResult.Success = true;
                    supplierServiceResult.Message = "Suplidor eliminado.";
                }
                else
                {
                    supplierServiceResult.Message = "El suplidor no existe.";
                    supplierServiceResult.Success = false;
                }

            }
            catch (Exception e)
            {
                _ILogger.LogError($"Error eliminando el suplidor. {e.Message}");
                supplierServiceResult.Success = false;
                supplierServiceResult.Message = "Error eliminando el suplidor";
            }
            return supplierServiceResult;
        }
    }
}
