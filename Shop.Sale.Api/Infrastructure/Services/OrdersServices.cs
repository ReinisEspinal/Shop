

using Microsoft.Extensions.Logging;
using Shop.Sale.Api.Infrastructure.Repository.Contracts;
using Shop.Sale.Api.Infrastructure.Services.Contracts;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Shop.Sale.Api.Infrastructure.Services.Core;
using System;
using System.Linq;
using Shop.Sale.Api.Infrastructure.Services.Models.Orders;
using Shop.Sale.Api.Data.Entities;

namespace Shop.Sale.Api.Infrastructure.Services
{
    public class OrdersServices : IOrdersServices
    {
        private readonly IOrdersRepository _IOrdersRepository;
        private readonly ICustomersRepository _ICustomersRepository;
        private readonly IEmployeesRepository _IEmployeesRepository;
        private readonly IShippersRepository _IShippersRepository;
        private readonly ILogger<Orders> _ILogger;
        private readonly IConfiguration _IConfiguration;

        public OrdersServices(IOrdersRepository iOrdersRepository,
            ICustomersRepository iCustomersRepository,
            IEmployeesRepository iEmployeesRepository,
            IShippersRepository iShippersRepository,
            ILogger<Orders> iLogger,
            IConfiguration iConfiguration)
        {
            this._IOrdersRepository = iOrdersRepository;
            this._ICustomersRepository = iCustomersRepository;
            this._IEmployeesRepository = iEmployeesRepository;
            this._IShippersRepository = iShippersRepository;
            this._ILogger = iLogger;
            this._IConfiguration = iConfiguration;
        }

        public async Task<OrdersServicesResponse> DeleteOrder(int id)
        {
            OrdersServicesResponse serviceResult = new OrdersServicesResponse();
            try
            {
                OrdersDeleteModel orderDeleteModel = new OrdersDeleteModel();

                var oOrderDelete = await _IOrdersRepository.GetById(id);

                oOrderDelete.UserDeleted = orderDeleteModel.UserDeleted;
                oOrderDelete.DeletedDate = orderDeleteModel.DeletedDate;
                oOrderDelete.Deleted = orderDeleteModel.Deleted;

                _IOrdersRepository.Update(oOrderDelete);
                await _IOrdersRepository.Commit();

                serviceResult.Data = oOrderDelete;
                serviceResult.Success = true;
            }
            catch (Exception e)
            {
                _ILogger.LogError($"{e.Message}");
                serviceResult.Success = false;
                serviceResult.Message = "Error eliminando la orden";

            }
            return serviceResult;
        }

        public async Task<OrdersServicesResponse> EditOrder(OrdersEditModel ordersEditModel)
        {
            OrdersServicesResponse serviceResult = new OrdersServicesResponse();

            try
            {
                var oOrder = await _IOrdersRepository.GetById(ordersEditModel.OrderId);

                oOrder.CustId = ordersEditModel.CustomerId;
                oOrder.EmpId = ordersEditModel.EmployeeId;
                oOrder.ShipperId = ordersEditModel.ShipperId;
                oOrder.OrderDate = ordersEditModel.OrderDate;
                oOrder.RequiredDate = ordersEditModel.RequiredDate;
                oOrder.ShippedDate = ordersEditModel.ShippedDate;
                oOrder.Freight = ordersEditModel.Freight;
                oOrder.ShipName = ordersEditModel.ShipName;
                oOrder.ShipAddress = ordersEditModel.ShipAddress;
                oOrder.ShipCity = ordersEditModel.ShipCity;
                oOrder.ShipRegion = ordersEditModel.ShipRegion;
                oOrder.ShipPostalCode = ordersEditModel.ShipPostalCode;
                oOrder.ShipCountry = ordersEditModel.ShipCountry;
                oOrder.ModifyDate = ordersEditModel.ModifyDate;
                oOrder.UserMod = ordersEditModel.UserMod;

                _IOrdersRepository.Update(oOrder);
                await _IOrdersRepository.Commit();
                serviceResult.Data = oOrder;
                serviceResult.Success = true;
            }
            catch (Exception e)
            {
                _ILogger.LogError($"{e.Message}");
                serviceResult.Message = "Error guardando la orden.";
                serviceResult.Success = false;

            }
            return serviceResult;
        }

        public OrdersServicesResponse GetOrderById(int id)
        {
            OrdersServicesResponse serviceResult = new OrdersServicesResponse();
            try
            {
                var query = (from orders in _IOrdersRepository.FindAll().Where(c => c.OrderId == id)
                             join
employees in _IEmployeesRepository.FindAll() on
orders.EmpId equals employees.EmpId
                             join
shippers in _IShippersRepository.FindAll() on
orders.ShipperId equals shippers.ShipperId
                             join
customers in _ICustomersRepository.FindAll() on
orders.CustId equals customers.CustId
                             select new OrdersGetModel
                             {
                                 OrderId = orders.OrderId,
                                 CustomerName = customers.ContactName,
                                 EmployeeName = employees.FirstName + " " + employees.LastName,
                                 ShipperName = shippers.CompanyName,
                                 OrderDate = orders.OrderDate,
                                 RequireDate = orders.RequiredDate,
                                 ShippedDate = orders.ShippedDate,
                                 Freight = orders.Freight,
                                 ShipAddress = orders.ShipAddress,
                                 ShipCity = orders.ShipCity,
                                 ShipRegion = orders.ShipRegion,
                                 ShipPostalCode = orders.ShipPostalCode,
                                 ShipCountry = orders.ShipCountry
                             });

                serviceResult.Data = query;
                serviceResult.Success = true;
            }
            catch (Exception e)
            {
                _ILogger.LogError($"{e.Message}");
                serviceResult.Success = false;
                serviceResult.Message = "Error obteniendo la order";
            }
            return serviceResult;
        }

        public OrdersServicesResponse GetOrders()
        {
            OrdersServicesResponse servicesResponse = new OrdersServicesResponse();

            try
            {
                var query = (from orders in _IOrdersRepository.FindAll()
                             join
customers in _ICustomersRepository.FindAll() on
orders.CustId equals customers.CustId
                             join
employees in _IEmployeesRepository.FindAll() on
orders.EmpId equals employees.EmpId
                             join
shippers in _IShippersRepository.FindAll() on
orders.ShipperId equals shippers.ShipperId
                             select new OrdersGetModel
                             {
                                 OrderId = orders.OrderId,
                                 EmployeeName = employees.FirstName,
                                 CustomerName = customers.ContactName,
                                 ShipperName = shippers.CompanyName,
                                 OrderDate = orders.OrderDate,
                                 RequireDate = orders.RequiredDate,
                                 ShippedDate = orders.ShippedDate,
                                 Freight = orders.Freight,
                                 ShipAddress = orders.ShipAddress,
                                 ShipCity = orders.ShipCity,
                                 ShipRegion = orders.ShipRegion,
                                 ShipPostalCode = orders.ShipPostalCode,
                                 ShipCountry = orders.ShipCountry
                             }
                              );


                servicesResponse.Data = query;
            }
            catch (Exception e)
            {
                _ILogger.LogError($"{e.Message}");
                servicesResponse.Message = "Error obteniendo las ordenes";
            }

            return servicesResponse;
        }

        public async Task<OrdersServicesResponse> SaveOrder(OrdersAddModel orderAddModel)
        {
            OrdersServicesResponse servicesResult = new OrdersServicesResponse();

            try
            {
                var newOrder = new Orders()
                {
                    CustId = orderAddModel.CustomerId,
                    EmpId = orderAddModel.EmployeeId,
                    ShipperId = orderAddModel.ShipperId,
                    OrderDate = orderAddModel.OrderDate,
                    RequiredDate = orderAddModel.RequiredDate,
                    ShippedDate = orderAddModel.ShippedDate,
                    Freight = orderAddModel.Freight,
                    ShipName = orderAddModel.ShipName,
                    ShipAddress = orderAddModel.ShipAddress,
                    ShipCity = orderAddModel.ShipCity,
                    ShipRegion = orderAddModel.ShipRegion,
                    ShipPostalCode = orderAddModel.ShipPostalCode,
                    ShipCountry = orderAddModel.ShipCountry,
                    CreationUser = orderAddModel.CreationUser
                };

                await _IOrdersRepository.Add(newOrder);
                await _IOrdersRepository.Commit();

                servicesResult.Success = true;
                servicesResult.Data = newOrder;
            }
            catch (Exception e)
            {
                _ILogger.LogError($"{e.Message}");
                servicesResult.Success = true;
                servicesResult.Message = "Error guardando los datos";
            }
            return servicesResult;
        }
    }
}