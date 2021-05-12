using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shop.Sale.Api.Infrastructure.Repository.Contracts;
using Shop.Sale.Api.Infrastructure.Services.Contracts;
using Shop.Sale.Api.Infrastructure.Services.Core;
using Shop.Sale.Api.Infrastructure.Services.Models.OrderDetails;
using Shop.Sale.Api.Infrastructure.Services.Models.Orders;
using System;
using System.Linq;

namespace Shop.Sale.Api.Infrastructure.Service
{
    public class OrderDetailsServices : IOrderDetailsServices
    {
        private readonly IOrderDetailsRepository _OrderDetailsRepository;
        private readonly IProductRepository _IProductRepository;
        private readonly ILogger<OrderDetailsServices> _ILogger;
        private readonly IConfiguration _Configuration;

        public OrderDetailsServices(IOrderDetailsRepository iOrderDetailsRepository,
            IProductRepository iProductReposiory,
                                    ILogger<OrderDetailsServices> iLogger,
                                    IConfiguration iConfiguration)
        {
            this._OrderDetailsRepository = iOrderDetailsRepository;
            this._IProductRepository = iProductReposiory;
            this._ILogger = iLogger;
            this._Configuration = iConfiguration;
        }

        public OrderDetailsServicesResponse DeleteOrderDelete(OrderDetailsDeleteModel orderDetailsDeleteModel)
        {
            throw new NotImplementedException();
        }

        public OrderDetailsServicesResponse EditOrderDetails(OrderDetailsEditModel orderDetailsEditModel)
        {
            throw new NotImplementedException();
        }

        public OrderDetailsServicesResponse GetOrderDetails()
        {
            OrderDetailsServicesResponse resultServiceResponse = new OrderDetailsServicesResponse();
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
                                 Discount = OrderDetails.Discount,
                                 QTY = OrderDetails.QTY
                             }
                    );;

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

        public OrderDetailsServicesResponse GetOrderDetailsById(int id)
        {
            throw new NotImplementedException();
        }

        public OrderDetailsServicesResponse SaveOrderDetails(OrderDetailsAddModel orderDetailsAddModel)
        {
            throw new NotImplementedException();
        }
    }
}
