using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shop.Sale.Api.Infraestructure.Repository.Contracts;
using Shop.Sale.Api.Infraestructure.Service.Contracts;
using Shop.Sale.Api.Infraestructure.Service.Core;
using Shop.Sale.Api.Infraestructure.Service.Models.Orders;
using System;
using System.Linq;

namespace Shop.Sale.Api.Infraestructure.Service
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly IOrderDetailsRepository _OrderDetailsRepository;
        private readonly IProductRepository _IProductRepository;
        private readonly ILogger<OrderDetailsService> _ILogger;
        private readonly IConfiguration _Configuration;

        public OrderDetailsService(IOrderDetailsRepository iOrderDetailsRepository,
                                    ILogger<OrderDetailsService> iLogger,
                                    IConfiguration iConfiguration)
        {
            this._OrderDetailsRepository = iOrderDetailsRepository;
            this._ILogger = iLogger;
            this._Configuration = iConfiguration;
        }

        public OrderDetailsServiceResponse GetOrderDetails()
        {
            OrderDetailsServiceResponse resultServiceResponse = new OrderDetailsServiceResponse();
            try
            {
                var query = (from OrderDetails in _OrderDetailsRepository.FindAll()
                             join Product in _IProductRepository.FindAll()
                             on OrderDetails.ProductId equals Product.ProductId

                             select new OrderDetailsGetModel
                             {
                                 OrderId = OrderDetails.OrderId,
                                 ProductName = Product.ProductName,
                                 UnitPrice = OrderDetails.UnitPrice,
                                 Discount = OrderDetails.Discount
                             }
                    );

                resultServiceResponse.Data = query;
                resultServiceResponse.Success = true;
            }
            catch (Exception e)
            {

                _ILogger.LogError($"{e.Message}");
                resultServiceResponse.Success = false;
                resultServiceResponse.Message = "Error obteniendo los detalles de las ordenes";
            }
            return resultServiceResponse;
        }
    }
}
