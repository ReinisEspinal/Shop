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
        public readonly ICategoryRepository _CategoryRepository;
        public readonly ILogger<ProductService> _logger;
        private readonly IConfiguration _Configuration;

        public ProductService(IProductRepository productRepository,
                              ISupplierRepository supplierRepository,
                              ICategoryRepository categoryRepository,
                               ILogger<ProductService> logger,
                              IConfiguration configuration)
        {
            this._ProductRepository = productRepository;
            this._SupplierRepository = supplierRepository;
            this._CategoryRepository = categoryRepository;
            this._Configuration = configuration;
            this._logger = logger;
        }

        public ProductServiceResultCore GetProducts()
        {
            ProductServiceResultCore productServiceResult = new ProductServiceResultCore();
            try
            {
                var query = (from product in _ProductRepository.FindAll()
                             join supplier in _SupplierRepository.FindAll()
                             on product.SupplierId equals supplier.SupplierId
                             join category in _CategoryRepository.FindAll()
                             on product.CategoryId equals category.CategoryId
                             select new ProductServiceResultGetModel
                             {
                                 ProductId = product.ProductId,
                                 ProductName = product.ProductName,
                                 CategoryName = category.CategoryName,
                                 CompanyName = supplier.CompanyName,
                                 UnitPrice = product.UnitPrice,
                                 Discontinued = product.Discontinued,
                             }).ToList();

                productServiceResult.Data = query;
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
                productServiceResult.Data = newProduct;
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
                if (await ValidateProduct(oProductServiceResultModifyModel.ProductName))
                {
                    resultProduct.Success = false;
                    resultProduct.Message = $"Este modelo {oProductServiceResultModifyModel.ProductName} ya esta registrado";
                    return resultProduct;
                }

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

                if (oProduct.Deleted == true)
                {
                    productServiceResult.Message = "El producto no existe";
                    return productServiceResult;
                }

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
                    productServiceResult.Success = true;
                    return productServiceResult;
                }
                else
                {

                    productServiceResult.Message = "El producto esta eliminado";
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