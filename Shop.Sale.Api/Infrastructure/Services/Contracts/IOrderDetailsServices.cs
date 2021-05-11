using Shop.Sale.Api.Infrastructure.Services.Core;

namespace Shop.Sale.Api.Infrastructure.Services.Contracts
{
    public interface IOrderDetailsServices
    {
        OrderDetailsServicesResponse GetOrderDetails();
    }
}
