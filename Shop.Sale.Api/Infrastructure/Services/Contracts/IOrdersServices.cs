using Shop.Sale.Api.Infrastructure.Services.Core;
using Shop.Sale.Api.Infrastructure.Services.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infrastructure.Services.Contracts
{
    public interface IOrdersServices
    {
        OrdersServicesResponse GetOrders();
        OrdersServicesResponse GetOrderById(int id);
        Task<OrdersServicesResponse> SaveOrder(OrdersAddModel orderAddModel);
        Task<OrdersServicesResponse> EditOrder(OrdersEditModel orderEditModel);
        Task<OrdersServicesResponse> DeleteOrder(int id);
    }
}
