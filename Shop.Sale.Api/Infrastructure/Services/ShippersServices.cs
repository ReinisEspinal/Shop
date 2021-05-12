using Shop.Sale.Api.Infrastructure.Services.Contracts;
using Shop.Sale.Api.Infrastructure.Repository.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Shop.Sale.Api.Data.Entities;
using System;
using Shop.Sale.Api.Infrastructure.Services.Core;
using System.Linq;
using Shop.Sale.Api.Infrastructure.Services.Models.Shipper;
using System.Threading.Tasks;
using Shop.Sale.Api.Infrastructure.Services.Models.Shippers;

namespace Shop.Sale.Api.Infrastructure.Service
{
    public class ShippersServices : IShippersServices
    {
        private readonly IShippersRepository _ShipperRepository;
        private readonly ILogger<Shippers> _ILogger;
        private readonly IConfiguration _IConfiguration;
        public ShippersServices(IShippersRepository iShipperRepository,
                              ILogger<Shippers> iLogger,
                              IConfiguration iConfiguration)
        {
            this._ShipperRepository = iShipperRepository;
            this._ILogger = iLogger;
            this._IConfiguration = iConfiguration;
        }

        public ShippersServicesResponse GetShippers()
        {
            ShippersServicesResponse shipperServiceResult = new ShippersServicesResponse();

            try
            {
                var query = (from shipper in _ShipperRepository.FindAll()
                             select new ShippersGetModel()
                             {
                                 ShipperId = shipper.ShipperId,
                                 CompanyName = shipper.CompanyName,
                                 Phone = shipper.Phone
                             });
                shipperServiceResult.Data = query;
                shipperServiceResult.Success = true;
            }
            catch (Exception e)
            {
                shipperServiceResult.Message = "Error obteniendo los datos de Shipper";
                shipperServiceResult.Success = false;
                _ILogger.LogError($"Error obteniendo los datos de Shipper{e.Message}");
            }

            return shipperServiceResult;
        }
        public ShippersServicesResponse GetShipperById(int id)
        {
            ShippersServicesResponse shippersServicesResponse = new ShippersServicesResponse();

            try
            {
                var query = (from shipper in _ShipperRepository.FindAll().Where(P => P.ShipperId == id)
                             select new ShippersGetModel
                             {
                                 ShipperId = shipper.ShipperId,
                                 CompanyName = shipper.CompanyName,
                                 Phone = shipper.Phone
                             });

                shippersServicesResponse.Data = query;
            }
            catch (Exception e)
            {
                _ILogger.LogError($"{e.Message}");
                shippersServicesResponse.Message = "Error consultando Shipper por ID";
                shippersServicesResponse.Success = false;
            }

            return shippersServicesResponse;
        }
        public async Task<ShippersServicesResponse> SaveShipper(ShippersAddModel shippersAddModel)
        {
            ShippersServicesResponse shippersServicesResponse = new ShippersServicesResponse();

            try
            {
                var shipper = new Shippers()
                {
                    CompanyName = shippersAddModel.CompanyName,
                    Phone = shippersAddModel.Phone,
                    CreationUser = shippersAddModel.CreateUser
                };

                await _ShipperRepository.Add(shipper);
                await _ShipperRepository.Commit();

                shippersServicesResponse.Data = shipper;
                shippersServicesResponse.Message = "Shipper guardado";
                shippersServicesResponse.Success = true;
            }
            catch (Exception e)
            {
                _ILogger.LogError($"{e.Message}");
                shippersServicesResponse.Message = "Error guardando shipper";
                shippersServicesResponse.Success = false;
            }
            return shippersServicesResponse;
        }
        public async Task<ShippersServicesResponse> DeleteShipper(ShippersDeleteModel shipperDeleteModel)
        {
            ShippersServicesResponse shipperServicesResponse = new ShippersServicesResponse();
            try
            {
                var oShipper = await _ShipperRepository.GetById(shipperDeleteModel.ShipperId);

                oShipper.UserDeleted = shipperDeleteModel.UserDeleted;
                oShipper.Deleted = shipperDeleteModel.Deleted;
                oShipper.DeletedDate = shipperDeleteModel.DeletedDate;

                _ShipperRepository.Update(oShipper);

                await _ShipperRepository.Commit();

                shipperServicesResponse.Message = "Shipper eliminado";
                shipperServicesResponse.Success = true;
                shipperServicesResponse.Data = oShipper;
            }
            catch (Exception e)
            {
                _ILogger.LogError($"{e.Message}");
                shipperServicesResponse.Message = "Error eliminando Shipper";
                shipperServicesResponse.Success = false;
            }
            return shipperServicesResponse;
        }

        public async Task<ShippersServicesResponse> EditShipper(ShippersEditModel shipperEditModel)
        {
            ShippersServicesResponse shippersServicesResponse = new ShippersServicesResponse();

            try
            {
                var oShipper = await _ShipperRepository.GetById(shipperEditModel.ShipperId);

                oShipper.CompanyName = shipperEditModel.CompanyName;
                oShipper.Phone = shipperEditModel.Phone;
                oShipper.UserMod = shipperEditModel.UserMod;
                oShipper.ModifyDate = shipperEditModel.ModifyDate;

                _ShipperRepository.Update(oShipper);
                await _ShipperRepository.Commit();

                shippersServicesResponse.Data = oShipper;
                shippersServicesResponse.Message = "Shipper editado";
                shippersServicesResponse.Success = true;

            }
            catch (Exception e)
            {
                _ILogger.LogError($"{e.Message}");
                shippersServicesResponse.Message = "Error editando Shipper";
                shippersServicesResponse.Success = false;
            }
            return shippersServicesResponse;
        }

    }
}