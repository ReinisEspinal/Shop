using Shop.Sale.Api.Infrastructure.Services.Contracts;
using Shop.Sale.Api.Infrastructure.Repository.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Shop.Sale.Api.Data.Entities;
using System;
using Shop.Sale.Api.Infrastructure.Services.Core;
using System.Linq;
using Shop.Sale.Api.Infrastructure.Services.Models.Shipper;

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
                             select new ShippersGetModel(){
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
  
    
    }
}
