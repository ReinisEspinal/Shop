using Shop.Sale.Api.Infraestructure.Service.Contracts;
using Shop.Sale.Api.Infraestructure.Repository.Contracts;
using Microsoft.Extensions.Logging;
using Shop.Sale.Api.Data.Entity;
using Microsoft.Extensions.Configuration;
using Shop.Sale.Api.Infraestructure.Service.Core;
using System;
using Shop.Sale.Api.Infraestructure.Service.Models.Customers;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infraestructure.Service
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _ICustomersRepository;
        private readonly ILogger<Customers> _ILogger;
        private readonly IConfiguration _IConfiguration;
        public CustomersService(ICustomersRepository _iCustomersRepository,
                                ILogger<Customers> _ILogger,
                                IConfiguration _Configuration)
        {
            this._ICustomersRepository = _iCustomersRepository;
            this._ILogger = _ILogger;
            this._IConfiguration = _Configuration;
        }

        public CustomersServiceResponse GetCustomers()
        {
            CustomersServiceResponse customersResult = new CustomersServiceResponse();

            try
            {
                var query = (from customers in _ICustomersRepository.FindAll()
                             select new CustomersGetModel
                             {
                                 CustomerId = customers.CustId,
                                 CompanyName = customers.CompanyName,
                                 ContactTitle = customers.ContactTitle,
                                 Address = customers.Address,
                                 City = customers.City,
                                 Region = customers.Region,
                                 PostalCode = customers.PostalCode,
                                 Country = customers.Country,
                                 Phone = customers.Phone,
                                 Fax = customers.Fax
                             });

                customersResult.Data = query;
                customersResult.Success = true;
            }
            catch (Exception e)
            {
                _ILogger.LogError($"Error obteniendo los datos. {e.Message}");
                customersResult.Message = "Error obteniendo los datos";
                customersResult.Success = false;
            }
            return customersResult;
        }

        public async Task<CustomersServiceResponse> SaveCustomer(CustomersAddModel oCustomerServiceResultModel)
        {
            CustomersServiceResponse customerServiceResult = new CustomersServiceResponse();

            try
            {
                if (await ValidateProduct(oCustomerServiceResultModel.ContactName))
                {
                    customerServiceResult.Success = false;
                    customerServiceResult.Message = $"Este cliente ya existe {oCustomerServiceResultModel.ContactName} ya esta registrado";
                    return customerServiceResult;
                }

                Customers newProduct = new Customers()
                {
                   ContactName = oCustomerServiceResultModel.ContactName

                };
                await _ICustomersRepository.Add(newProduct);
                await _ICustomersRepository.Commit();

                customerServiceResult.Success = true;
                customerServiceResult.Data = oCustomerServiceResultModel;
                customerServiceResult.Message = "Cliente agregado";

            }
            catch (Exception e)
            {
                _ILogger.LogError($"{e.Message}");
                customerServiceResult.Success = false;
                customerServiceResult.Message = "Error agregando el cliente";
                customerServiceResult.Data = null;
            }
            return customerServiceResult;
        }

        public async Task<CustomersServiceResponse> UpdateCustomer(CustomersModifyModel oCustomerServiceResultModifyModel)
        {
            CustomersServiceResponse customerResult = new CustomersServiceResponse();

            try
            {
                Customers customersUpdated = await _ICustomersRepository.GetById(oCustomerServiceResultModifyModel.CustId);

                if (customersUpdated == null || customersUpdated.Deleted == true)
                {
                    customerResult.Message = "el cliente no existe";
                    return customerResult;
                }
                else
                {
                    if (await ValidateProduct(oCustomerServiceResultModifyModel.ContactName))
                    {

                        customerResult.Message = $"Este cliente '{oCustomerServiceResultModifyModel.ContactName}' ya esta registrado";
                        customerResult.Success = false;
                    }
                    else
                    {
                        customersUpdated.ContactName = oCustomerServiceResultModifyModel.ContactName;


                        _ICustomersRepository.Update(customersUpdated);
                        await _ICustomersRepository.Commit();

                        customerResult.Data = customersUpdated;
                        customerResult.Message = "Cliente actualizado correctamente.";
                    }

                }
            }
            catch (Exception e)
            {
                _ILogger.LogError($"Error:{e.Message}");
                customerResult.Success = false;
                customerResult.Message = "Error agregando la informacion del cliente";
            }
            return customerResult;
        }
        public async Task<CustomersServiceResponse> RemoveCustomer(int id)
        {
            CustomersServiceResponse customerServiceResult = new CustomersServiceResponse();
            CustomersDeletedModel customerDeleteModel = new CustomersDeletedModel();
            try
            {
                Customers oCustomer = await _ICustomersRepository.GetById(id);

                if (oCustomer == null || oCustomer.Deleted == true)
                {
                    customerServiceResult.Message = "el cliente no existe";
                    return customerServiceResult;
                }
                oCustomer.Deleted = true;
                oCustomer.UserDeleted = customerDeleteModel.UserDeleted;
                oCustomer.DeletedDate = customerDeleteModel.DeletedDate;

                _ICustomersRepository.Update(oCustomer);
                await _ICustomersRepository.Commit();

                customerServiceResult.Success = true;
                customerServiceResult.Message = "Producto eliminado";

            }
            catch (Exception e)
            {
                _ILogger.LogError($"{e.Message}");
                customerServiceResult.Success = false;
                customerServiceResult.Message = "Error eliminando el cliente";
            }
            return customerServiceResult;
        }
        public async Task<CustomersServiceResponse> GetCustomerById(int id)
        {
            CustomersServiceResponse customerServiceResult = new CustomersServiceResponse();
            CustomersGetModel productGetModel = new CustomersGetModel();
            try
            {
                var oCustomer = await _ICustomersRepository.GetById(id);


                if (oCustomer == null || oCustomer.Deleted == true)
                {
                    customerServiceResult.Message = "el cliente no existe";
                    customerServiceResult.Data = null;
                    customerServiceResult.Success = true;
                    return customerServiceResult;
                }
                else
                {
                    var query = (from customer in _ICustomersRepository.FindAll()
                                 select new CustomersGetModel
                                 {
                                     CustomerId = customer.CustId,
                                   ContactName = customer.ContactName

                                 });
                    customerServiceResult.Data = query;
                    customerServiceResult.Message = "Cliente encontrado";
                    customerServiceResult.Success = true;
                }
            }
            catch (Exception e)
            {
                _ILogger.LogError($"{e.Message}");
                customerServiceResult.Message = "Error filtrando el cliente";
                customerServiceResult.Success = false;
            }
            return customerServiceResult;
        }

        public async Task<bool> ValidateProduct(string customerName)
        {

            return await _ICustomersRepository.Exists(Customer => Customer.ContactName == customerName);

        }

    }
}
