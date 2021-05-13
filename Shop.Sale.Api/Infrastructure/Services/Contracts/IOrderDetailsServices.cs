using Shop.Sale.Api.Infrastructure.Services.Core;
using Shop.Sale.Api.Infrastructure.Services.Models.OrderDetails;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infrastructure.Services.Contracts
{
    public interface IOrderDetailsServices
    {
        OrderDetailsServicesResponse GetOrderDetails();
        OrderDetailsServicesResponse GetOrderDetailsById(int id);
        OrderDetailsServicesResponse SaveOrderDetails(OrderDetailsAddModel orderDetailsAddModel);
        OrderDetailsServicesResponse EditOrderDetails(OrderDetailsEditModel orderDetailsEditModel);
        Task <OrderDetailsServicesResponse> DeleteAllOrderDetails(OrderDetailsDeleteModel orderDetailsDeleteModel);
    }
}
