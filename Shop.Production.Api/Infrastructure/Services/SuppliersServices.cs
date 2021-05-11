using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shop.Production.Api.Data.Entities;
using Shop.Production.Api.Infrastructure.Repository.Contracts;
using Shop.Production.Api.Infrastructure.Services.Contracts;
using Shop.Production.Api.Infrastructure.Services.Core;
using Shop.Production.Api.Infrastructure.Services.Models.Supplier;
using System;
using System.Threading.Tasks;

namespace Shop.Production.Api.Infrastructure.Services
{
    public class SuppliersServices : ISuppliersServices
    {
        private readonly ISuppliersRepository _ISuppliersRepository;
        private readonly ILogger<SuppliersServices> _ILogger;
        private readonly IConfiguration _IConfiguration;

        public SuppliersServices(ISuppliersRepository iSupplierRepository, 
            ILogger<SuppliersServices> iLogger, 
            IConfiguration iConfiguration)
        {
            this._ISuppliersRepository = iSupplierRepository;
            this._ILogger = iLogger;
            this._IConfiguration = iConfiguration;
        }
        public SuppliersServicesResponse GetSuppliers()
        {
            SuppliersServicesResponse supplierServiceResult = new SuppliersServicesResponse();
            try
            {
                var oSupplier = _ISuppliersRepository.FindAll();
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
        public async Task<SuppliersServicesResponse> GetSupplierById(int id)
        {
            var supplierServiceResult = new SuppliersServicesResponse();
            try
            {
                supplierServiceResult.Data = await _ISuppliersRepository.GetById(id);
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

        public async Task<SuppliersServicesResponse> SaveSupplier(SuppliersAddModel supplierAddModel)
        {
            SuppliersServicesResponse supplierServicesResult = new SuppliersServicesResponse();

            try
            {
                Suppliers oSupplier = new Suppliers()
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
                await _ISuppliersRepository.Add(oSupplier);
                await _ISuppliersRepository.Commit();

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

        //Si es de mucho request no usar esta manera
        //La otra forma es utilizar storeprocedure para controlar el performance
        //Command text???

        public async Task<SuppliersServicesResponse> UpdateSupplier(SuppliersModifyModel supplierModifyModel)
        {
            SuppliersServicesResponse supplierServiceResult = new SuppliersServicesResponse();

            try
            {
                var supplieUpdate = await _ISuppliersRepository.GetById(supplierModifyModel.SupplierId);
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

                _ISuppliersRepository.Update(supplieUpdate);

                await _ISuppliersRepository.Commit();
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

        public async Task<SuppliersServicesResponse> DeleteSupplier(int id)
        {
            SuppliersServicesResponse supplierServiceResult = new SuppliersServicesResponse();
            var supplierDeleteModel = new SuppliersDeleteModel();

            try
            {
                var oSupplier = await _ISuppliersRepository.Find(c => c.SupplierId == id);
                if (!oSupplier.Deleted)
                {

                    oSupplier.UserDeleted = supplierDeleteModel.UserDeleted;
                    oSupplier.DeletedDate = supplierDeleteModel.DeletedDate;
                    oSupplier.Deleted = supplierDeleteModel.Deleted;

                    _ISuppliersRepository.Update(oSupplier);

                    await _ISuppliersRepository.Commit();

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
