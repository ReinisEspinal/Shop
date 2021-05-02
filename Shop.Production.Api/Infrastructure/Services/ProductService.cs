using Shop.Production.Api.Infrastructure.Services.Contracts;
using Shop.Production.Api.Infrastructure.Repository.Contracts;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Shop.Production.Api.Infrastructure.Services.ServicesResult.Core;
using System;
using System.Linq;
using Shop.Production.Api.Infrastructure.Services.ServicesResult.Models;
using Microsoft.Extensions.Logging;
using Shop.Production.Api.Infrastructure.Data.Entities;
using Shop.Production.Api.Infrastructure.Services.ServicesResult.Models.Product;

namespace Shop.Production.Api.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        public readonly IProductRepository _ProductRepository;
        public readonly ISupplierRepository _SupplierRepository;
        public readonly ILogger<ProductService> _logger;
        private readonly IConfiguration _Configuration;

        public ProductService(IProductRepository productRepository,
                              ISupplierRepository supplierRepository,

                               ILogger<ProductService> logger,
                              IConfiguration configuration)
        {
            this._ProductRepository = productRepository;
            this._SupplierRepository = supplierRepository;
            this._Configuration = configuration;
            this._logger = logger;
        }

        /// <summary>
        /// Metodo para obtener la lista de producto. Falta obtener el dato de categoria; hay que realizar el repositorio.
        /// </summary>
        /// <returns></returns>
        public ProductServiceResultCore GetProducts()
        {
            ProductServiceResultCore productServiceResult = new ProductServiceResultCore();
            try
            {
                var products = _ProductRepository.FindAll(pProducts => !pProducts.Deleted);
                var suppliers = _SupplierRepository.FindAll(sSupplier => !sSupplier.Deleted);

                var query = (from product in products
                             join supplier in suppliers
                             on product.SupplierId equals supplier.SupplierId
                             select new ProductServiceResultGetModel
                             {
                                 ProductId = product.ProductId,
                                 ProductName = product.ProductName,
                                 CompanyName = supplier.CompanyName,
                                 SupplierId = product.SupplierId,
                                 CategoryId = product.CategoryId,
                                 UnitPrice = product.UnitPrice,
                                 Discontinued = product.Discontinued
                             }).ToList();

                productServiceResult.Data = query;
                productServiceResult.Message = "Lista de productos";
                productServiceResult.Success = true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error {e.Message}");
                productServiceResult.Success = false;
                productServiceResult.Message = "Error obteniendo los productos";
            }

            return productServiceResult;
        }
        public async Task<ProductServiceResultCore> SaveProduct(ProductServiceResultAddModel oProductServiceResultModel)
        {
            ProductServiceResultCore productServiceResult = new ProductServiceResultCore();

            try
            {
                if (await ValidateProduct(oProductServiceResultModel.ProductName))
                {
                    productServiceResult.Success = false;
                    productServiceResult.Message = $"Este modelo {oProductServiceResultModel.ProductName} ya esta registrado";
                    return productServiceResult;
                }

                Product newProduct = new Product()
                {
                    ProductName = oProductServiceResultModel.ProductName,
                    SupplierId = oProductServiceResultModel.SupplierId,
                    CategoryId = oProductServiceResultModel.CategoryId,
                    UnitPrice = oProductServiceResultModel.UnitPrice,
                    Discontinued = oProductServiceResultModel.Discontinued,
                    CreationUser = oProductServiceResultModel.CreationUser,
                    CreationDate = oProductServiceResultModel.CreationDate
                };
                await _ProductRepository.Add(newProduct);
                await _ProductRepository.Commit();

                productServiceResult.Success = true;
                productServiceResult.Message = "Producto agregado";

            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}");
                productServiceResult.Success = false;
                productServiceResult.Message = "Error agregando el producto";
                productServiceResult.Data = null;
            }
            return productServiceResult;
        }
        public async Task<ProductServiceResultCore> UpdateProduct(ProductServiceResultModifyModel oProductServiceResultModifyModel)
        {
            ProductServiceResultCore resultProduct = new ProductServiceResultCore();

            try
            {
                Product productUpdated = await _ProductRepository.GetById(oProductServiceResultModifyModel.ProductId);

                productUpdated.ProductName = oProductServiceResultModifyModel.ProductName;
                productUpdated.SupplierId = oProductServiceResultModifyModel.SupplierId;
                productUpdated.CategoryId = oProductServiceResultModifyModel.CategoryId;
                productUpdated.UnitPrice = oProductServiceResultModifyModel.UnitPrice;
                productUpdated.Discontinued = oProductServiceResultModifyModel.Discontinued;
                productUpdated.UserMod = oProductServiceResultModifyModel.UserMod;
                productUpdated.ModifyDate = oProductServiceResultModifyModel.ModifyDate;

                _ProductRepository.Update(productUpdated);
                await _ProductRepository.Commit();

                resultProduct.Data = productUpdated;
                resultProduct.Message = "Producto actualizado correctamente.";
            }
            catch (Exception e)
            {
                _logger.LogError($"Error:{e.Message}");
                resultProduct.Success = false;
                resultProduct.Message = "Error agregando la informacion del producto";
            }
            return resultProduct;
        }
        public async Task<ProductServiceResultCore> RemoveProduct(int id)
        {
            ProductServiceResultCore productServiceResult = new ProductServiceResultCore();
            ProductServiceResultDeletedModel productDeleteModel = new ProductServiceResultDeletedModel();
            try
            {
                Product oProduct = await _ProductRepository.GetById(id);

                oProduct.Deleted = true;
                oProduct.UserDeleted = productDeleteModel.UserDeleted;
                oProduct.DeletedDate = productDeleteModel.DeletedDate;

                _ProductRepository.Update(oProduct);
                await _ProductRepository.Commit();

                productServiceResult.Success = true;
                productServiceResult.Message = "Producto eliminado";

            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}");
                productServiceResult.Success = false;
                productServiceResult.Message = "Error eliminando el producto";
            }
            return productServiceResult;
        }
        public async Task<ProductServiceResultCore> GetProductById(int id)
        {
            ProductServiceResultCore productServiceResult = new ProductServiceResultCore();
            try
            {
                var oProduct = await _ProductRepository.GetById(id);

                if (!oProduct.Deleted)
                {
                    productServiceResult.Data = oProduct;
                    productServiceResult.Message = "Producto encontrado";
                }
                else
                {

                    productServiceResult.Message = "Producto eliminado";
                    productServiceResult.Data = null;
                    productServiceResult.Success = true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}");
                productServiceResult.Message = "Error filtrando el producto";
                productServiceResult.Success = false;
            }
            return productServiceResult;
        }
        public async Task<bool> ValidateProduct(string productName)
        {

            return await _ProductRepository.Exists(Product => Product.ProductName == productName);

        }

    }
}