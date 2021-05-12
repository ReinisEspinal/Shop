using Shop.Sale.Api.Data.Entities;
using Shop.Sale.Api.Infrastructure.Services.Core;
using Shop.Sale.Api.Infrastructure.Services.Models.Shippers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infrastructure.Services.Contracts
{
    public interface IShippersServices
    {
        ShippersServicesResponse GetShippers();

        ShippersServicesResponse GetShipperById(int shipperId);
        Task<ShippersServicesResponse> SaveShipper(ShippersAddModel shipperAdd);
        Task<ShippersServicesResponse> DeleteShipper(ShippersDeleteModel shipperId);
        Task<ShippersServicesResponse> EditShipper(ShippersEditModel shipperEdit);
    }
}

