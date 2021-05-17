using Shop.Sale.Api.Infrastructure.Services.Core;
using Shop.Sale.Api.Infrastructure.Services.Models.OrderDetails;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infrastructure.Services.Contracts
{
    public interface IOrderDetailsServices
    {
        OrderDetailsServicesResponse GetOrderDetails();
        OrderDetailsServicesResponse GetOrderDetailsById(int orderId, int productId);
        OrderDetailsServicesResponse GetOrderDetailsById(int orderId);
        Task<OrderDetailsServicesResponse> SaveOrderDetails(OrderDetailsAddModel orderDetailsAddModel);
        Task<OrderDetailsServicesResponse> EditOrderDetails(OrderDetailsEditModel orderDetailsEditModel);
        Task <OrderDetailsServicesResponse> DeleteOrderDetail(OrderDetailsDeleteModel orderDetailsDeleteModel);
    }
}
