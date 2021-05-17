using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shop.Sale.Api.Data.Entities;
using Shop.Sale.Api.Infrastructure.Repository.Contracts;
using Shop.Sale.Api.Infrastructure.Services.Contracts;
using Shop.Sale.Api.Infrastructure.Services.Core;
using Shop.Sale.Api.Infrastructure.Services.Models.OrderDetails;
using Shop.Sale.Api.Infrastructure.Services.Models.Orders;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infrastructure.Service
{
    public class OrderDetailsServices : IOrderDetailsServices
    {
        private readonly IOrderDetailsRepository _OrderDetailsRepository;
        private readonly IOrdersRepository _OrderRepository;
        private readonly IProductRepository _IProductRepository;
        private readonly IEmployeesRepository _IEmployeeRepository;
        private readonly ILogger<OrderDetailsServices> _ILogger;
        private readonly IConfiguration _Configuration;

        public OrderDetailsServices(IOrderDetailsRepository iOrderDetailsRepository,
            IOrdersRepository iOrderRepository,
            IProductRepository iProductReposiory,
            IEmployeesRepository iEmployeeRespository,
                                    ILogger<OrderDetailsServices> iLogger,
                                    IConfiguration iConfiguration)
        {
            this._OrderRepository = iOrderRepository;
            this._OrderDetailsRepository = iOrderDetailsRepository;
            this._IProductRepository = iProductReposiory;
            this._IEmployeeRepository = iEmployeeRespository;
            this._ILogger = iLogger;
            this._Configuration = iConfiguration;
        }

        public async Task<OrderDetailsServicesResponse> DeleteOrderDetail(OrderDetailsDeleteModel orderDetailsDeleteModel)
        {
            OrderDetailsServicesResponse orderDetailsSeviceResponse = new OrderDetailsServicesResponse();
            try
            {
                var orderDetails = _OrderDetailsRepository.FindAll().Where(c => c.OrderId == orderDetailsDeleteModel.OrderId);

                foreach (var item in orderDetails)
                {
                    if (item.ProductId == orderDetailsDeleteModel.ProductId)
                    {
                        item.UserDeleted = orderDetailsDeleteModel.UserDeleted;
                        item.Deleted = orderDetailsDeleteModel.Deleted;
                        item.DeletedDate = orderDetailsDeleteModel.DeletedDate;

                        break;
                    }
                }

                await _OrderDetailsRepository.Commit();
                orderDetailsSeviceResponse.Message = "Eliminado";
                orderDetailsSeviceResponse.Success = true;
                orderDetailsSeviceResponse.Data = orderDetailsDeleteModel;
            }
            catch (Exception e)
            {
                _ILogger.LogError($"{e.Message}");
                orderDetailsSeviceResponse.Success = false;
                orderDetailsSeviceResponse.Message = "Error eliminando el detalle de orden";

            }
            return orderDetailsSeviceResponse;
        }
        public async Task<OrderDetailsServicesResponse> EditOrderDetails(OrderDetailsEditModel orderDetailsEditModel)
        {

            OrderDetailsServicesResponse serviceResult = new OrderDetailsServicesResponse();

            try
            {
                var oOrderDetails = _OrderDetailsRepository.FindAll().Where(orderDetails => orderDetails.OrderId == orderDetailsEditModel.OrderId);

                foreach (var item in oOrderDetails)
                {
                    if (item.ProductId == orderDetailsEditModel.ProductId)
                    {
                        item.UnitPrice = orderDetailsEditModel.UnitPrice;
                        item.QTY = orderDetailsEditModel.QTY;
                        item.ModifyDate = orderDetailsEditModel.ModifyDate;
                        item.UserMod = orderDetailsEditModel.UserMod;
                        item.Discount = orderDetailsEditModel.Discount;

                        _OrderDetailsRepository.Update(item);
                        break;
                    }

                }

                await _OrderDetailsRepository.Commit();
                serviceResult.Data = orderDetailsEditModel;



                serviceResult.Message = "Orden del detalle editada";
                serviceResult.Success = true;
            }
            catch (Exception e)
            {
                _ILogger.LogError($"{e.Message}");
                serviceResult.Message = "Error editando el detalle de la orden";
                serviceResult.Success = false;
            }
            return serviceResult;
        }
        public OrderDetailsServicesResponse GetOrderDetails()
        {
            OrderDetailsServicesResponse resultServiceResponse = new OrderDetailsServicesResponse();
            try
            {
                var query = (from orderDetails in _OrderDetailsRepository.FindAll()
                             join order in _OrderRepository.FindAll()
                             on orderDetails.OrderId equals order.OrderId
                             join products in _IProductRepository.FindAll()
                             on orderDetails.ProductId equals products.ProductId
                             join
employee in _IEmployeeRepository.FindAll() on
order.EmpId equals employee.EmpId
                             select new OrderDetailsGetModel
                             {
                                 OrderId = orderDetails.OrderId,
                                 ProductName = products.ProductName,
                                 UnitPrice = orderDetails.UnitPrice,
                                 Discount = orderDetails.Discount,
                                 QTY = orderDetails.QTY,
                                 EmployeeName = employee.FirstName + " " + employee.LastName,
                                 OrderDate = order.OrderDate
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
        public OrderDetailsServicesResponse GetOrderDetailsById(int orderId, int productId)
        {
            OrderDetailsServicesResponse serviceResponse = new OrderDetailsServicesResponse();

            try
            {
                var query = (from orderDetails in _OrderDetailsRepository.FindAll().Where(order => order.OrderId == orderId).Where(product => product.ProductId == productId)
                             join order in _OrderRepository.FindAll()
                             on orderDetails.OrderId equals order.OrderId
                             join products in _IProductRepository.FindAll()
                             on orderDetails.ProductId equals products.ProductId
                             join
employee in _IEmployeeRepository.FindAll() on
order.EmpId equals employee.EmpId
                             select new OrderDetailsGetModel
                             {
                                 OrderId = orderDetails.OrderId,
                                 ProductName = products.ProductName,
                                 UnitPrice = orderDetails.UnitPrice,
                                 Discount = orderDetails.Discount,
                                 QTY = orderDetails.QTY,
                                 EmployeeName = employee.FirstName + " " + employee.LastName,
                                 OrderDate = order.OrderDate
                             });

                serviceResponse.Data = query;
                serviceResponse.Success = true;
            }
            catch (Exception e)
            {
                _ILogger.LogError($"{e.Message}");
                serviceResponse.Message = "Error obteniendo los datos";
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }
        public OrderDetailsServicesResponse GetOrderDetailsById(int orderId)
        {
            OrderDetailsServicesResponse serviceResponse = new OrderDetailsServicesResponse();

            try
            {
                var query = (from orderDetails in _OrderDetailsRepository.FindAll().Where(order => order.OrderId == orderId)
                             join order in _OrderRepository.FindAll()
                             on orderDetails.OrderId equals order.OrderId
                             join products in _IProductRepository.FindAll()
                             on orderDetails.ProductId equals products.ProductId
                             join
employee in _IEmployeeRepository.FindAll() on
order.EmpId equals employee.EmpId
                             select new OrderDetailsGetModel
                             {
                                 OrderId = orderDetails.OrderId,
                                 ProductName = products.ProductName,
                                 UnitPrice = orderDetails.UnitPrice,
                                 Discount = orderDetails.Discount,
                                 QTY = orderDetails.QTY,
                                 EmployeeName = employee.FirstName + " " + employee.LastName


                             }); ;

                serviceResponse.Data = query;
                serviceResponse.Success = true;
            }
            catch (Exception e)
            {
                _ILogger.LogError($"{e.Message}");
                serviceResponse.Message = "Error obteniendo los datos";
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<OrderDetailsServicesResponse> SaveOrderDetails(OrderDetailsAddModel orderDetailsAddModel)
        {
            OrderDetailsServicesResponse servicesResponse = new OrderDetailsServicesResponse();

            try
            {
                OrderDetails orderDetails = new OrderDetails()
                {

                    ProductId = orderDetailsAddModel.ProductId,
                    OrderId = orderDetailsAddModel.OrderId,

                    UnitPrice = orderDetailsAddModel.UnitPrice,
                    QTY = orderDetailsAddModel.QTY,
                    Discount = orderDetailsAddModel.Discount,
                    CreationUser = orderDetailsAddModel.CreationUser,
                    CreationDate = orderDetailsAddModel.CreationDate
                };



                await _OrderDetailsRepository.Add(orderDetails);
                await _OrderDetailsRepository.Commit();

                servicesResponse.Data = orderDetails;
                servicesResponse.Message = "Detalle de orden guardada";

            }
            catch (Exception e)
            {
                _ILogger.LogError($"{e.Message}");
                servicesResponse.Message = "Error guardando los datos";
                servicesResponse.Success = false;
            }
            return servicesResponse;
        }
    }
}