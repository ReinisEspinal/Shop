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
                             select new ProductServiceResultModel
                             {
                                 ProductId = product.ProductId,
                                 ProductName = product.ProductName,
                                 SupplierId = product.SupplierId,
                                 CategoryId = product.CategoryId,
                                 UnitPrice = product.UnitPrice,
                                 Discontinued = product.Discontinued,
                                 CreationDate = product.CreationDate,
                                 ModifyDate = product.ModifyDate,
                                 DeletedDate = product.DeletedDate,
                                 Deleted = product.Deleted
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
        public ProductServiceResultCore GetProductByID(int id)
        {
            ProductServiceResultCore productServiceResult = new ProductServiceResultCore();
            try
            {
                // Product oProduct = await _ProductRepository.GetById(id);
                // Supplier oSupplier = await _SupplierRepository.GetById(oProduct.SupplierId);

                var oProduct = _ProductRepository.FindAll(pProducts => !pProducts.Deleted);
                var oSupplier = _SupplierRepository.FindAll(sSupplier => !sSupplier.Deleted);
                var query = (from product in oProduct
                             join supplier in oSupplier
                             on product.SupplierId equals supplier.SupplierId
                             select new ProductServiceResultModel
                             {
                                 ProductId = product.ProductId,
                                 ProductName = product.ProductName,
                                 SupplierId = product.SupplierId,
                                 CategoryId = product.CategoryId,
                                 UnitPrice = product.UnitPrice,
                                 Discontinued = product.Discontinued,
                                 CreationDate = product.CreationDate,
                                 ModifyDate = product.ModifyDate,
                                 DeletedDate = product.DeletedDate,
                                 Deleted = product.Deleted,
                                 CompanyName = supplier.CompanyName

                             }).ToList();

                productServiceResult.Data = query;

                #region Comentario2
                //productServiceResult.Data = new ProductServiceResultModels()
                //{
                //    ProductId = product.ProductId,
                //    ProductName = product.ProductName,
                //    SupplierId = product.SupplierId,
                //    CategoryId = product.CategoryId,
                //    UnitPrice = product.UnitPrice,
                //    Discontinued = product.Discontinued,
                //    CreationDate = product.CreationDate,
                //    ModifyDate = product.ModifyDate,
                //    DeletedDate = product.DeletedDate,
                //    Deleted = product.Deleted
                //};
                #endregion

                productServiceResult.Success = true;
                productServiceResult.Message = "Producto obtenido";
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                productServiceResult.Success = true;
                productServiceResult.Message = "Error obteniendo los productos";
            }
            return  productServiceResult;
        }
        public async Task<ProductServiceResultCore> SaveProduct(ProductServiceResultModel oProductServiceResultModel)
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
                    ProductId = oProductServiceResultModel.ProductId,
                    ProductName = oProductServiceResultModel.ProductName,
                    SupplierId = oProductServiceResultModel.SupplierId,
                    CategoryId = oProductServiceResultModel.CategoryId,
                    UnitPrice = oProductServiceResultModel.UnitPrice,
                    Discontinued = oProductServiceResultModel.Discontinued,
                    CreationDate = oProductServiceResultModel.CreationDate,
                    ModifyDate = oProductServiceResultModel.ModifyDate,
                    DeletedDate = oProductServiceResultModel.DeletedDate,
                    Deleted = oProductServiceResultModel.Deleted
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

        public async Task<ProductServiceResultCore> UpdateProduct(ProductServiceResultModel oProductServiceResultModel)
        {
            ProductServiceResultCore resultProduct = new ProductServiceResultCore();

            try
            {
                var productUpdated =  await _ProductRepository.GetById(oProductServiceResultModel.ProductId);

                productUpdated.ProductName = oProductServiceResultModel.ProductName;
                productUpdated.SupplierId = oProductServiceResultModel.SupplierId;
                productUpdated.CategoryId = oProductServiceResultModel.CategoryId;
                productUpdated.UnitPrice = oProductServiceResultModel.UnitPrice;
                productUpdated.Discontinued = oProductServiceResultModel.Discontinued;

                _ProductRepository.Update(productUpdated);
                await _ProductRepository.Commit();
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
        public async Task<ProductServiceResultCore> RemoveProduct(ProductServiceResultDeletedModel productDeletedModel)
        {
            ProductServiceResultCore productServiceResult = new ProductServiceResultCore();
            try
            {
                Product oProduct = await _ProductRepository.GetById(productDeletedModel.ProductId);

                oProduct.Deleted = true;
                oProduct.UserDeleted = productDeletedModel.UserDeleted;
                oProduct.DeletedDate = productDeletedModel.DeletedDate;

                _ProductRepository.Update(oProduct);
                await _ProductRepository.Commit();

                productServiceResult.Success = true;
                productServiceResult.Message = "Producto consultado por ID";

            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}");
                productServiceResult.Success = false;
                productServiceResult.Message = "Error obteniendo los datos del producto";
            }
            return productServiceResult;
        }

        public async Task<bool> ValidateProduct(string productName)
        {

            return await _ProductRepository.Exists(Product => Product.ProductName == productName);

        }

    }
}