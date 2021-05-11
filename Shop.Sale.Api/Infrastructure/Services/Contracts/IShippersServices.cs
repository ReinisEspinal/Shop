using Shop.Sale.Api.Data.Entities;
using Shop.Sale.Api.Infrastructure.Services.Core;
using System.Collections.Generic;

namespace Shop.Sale.Api.Infrastructure.Services.Contracts
{
    public interface IShippersServices
    {
        ShippersServicesResponse GetShippers();

       
    }
}

