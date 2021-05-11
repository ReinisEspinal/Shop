using Shop.Production.Api.Infrastructure.Services.Contracts;
using Shop.Production.Api.Infrastructure.Repository.Contracts;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Shop.Production.Api.Data.Entities;
using Shop.Production.Api.Infrastructure.Services.Core;
using Shop.Production.Api.Infrastructure.Services.Models.Product;

namespace Shop.Production.Api.Infrastructure.Services
{
    public class ProductsServices : IProductsServices
    {
        public readonly IProductsRepository _ProductsRepository;
        public readonly ISuppliersRepository _SuppliersRepository;
        public readonly ICategoriesRepository _CategoriesRepository;
        public readonly ILogger<ProductsServices> _logger;
        private readonly IConfiguration _Configuration;

        public ProductsServices(IProductsRepository productsRepository,
                              ISuppliersRepository suppliersRepository,
                              ICategoriesRepository categoriesRepository,
                               ILogger<ProductsServices> logger,
                              IConfiguration configuration)
        {
            this._ProductsRepository = productsRepository;
            this._SuppliersRepository = suppliersRepository;
            this._CategoriesRepository = categoriesRepository;
            this._Configuration = configuration;
            this._logger = logger;
        }

        public ProductsServicesResponse GetProducts()
        {
            ProductsServicesResponse productServiceResult = new ProductsServicesResponse();
            try
            {
                var query = (from product in _ProductsRepository.FindAll()
                             join supplier in _SuppliersRepository.FindAll()
                             on product.SupplierId equals supplier.SupplierId
                             join category in _CategoriesRepository.FindAll()
                             on product.CategoryId equals category.CategoryId
                             select new ProductsGetModel
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

        /*
             Verifica todos los nombres de los productos tanto los eliminados como los existentes.
            Si existe y esta eliminado como quiera dira que existe.
        */
        public async Task<ProductsServicesResponse> SaveProduct(ProductsAddModel oProductServiceResultModel)
        {
            ProductsServicesResponse productServiceResult = new ProductsServicesResponse();

            try
            {
                if (await ValidateProduct(oProductServiceResultModel.ProductName))
                {
                    productServiceResult.Success = false;
                    productServiceResult.Message = $"Este modelo {oProductServiceResultModel.ProductName} ya esta registrado";
                    return productServiceResult;
                }

                Products newProduct = new Products()
                {
                    ProductName = oProductServiceResultModel.ProductName,
                    SupplierId = oProductServiceResultModel.SupplierId,
                    CategoryId = oProductServiceResultModel.CategoryId,
                    UnitPrice = oProductServiceResultModel.UnitPrice,
                    Discontinued = oProductServiceResultModel.Discontinued,
                    CreationUser = oProductServiceResultModel.CreationUser,
                    CreationDate = oProductServiceResultModel.CreationDate
                };
                await _ProductsRepository.Add(newProduct);
                await _ProductsRepository.Commit();

                productServiceResult.Success = true;
                productServiceResult.Data = oProductServiceResultModel;
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
        
        /*
         Puedo editar un cliente con una categoria que no existe, lo mejor es realizar una clase
        con las validaciones y llamar esta en el modelo del servicio para que no permita
        colocar datos de entidades que no existe o  que esten eliminadas
         */

        public async Task<ProductsServicesResponse> UpdateProduct(ProductsModifyModel oProductServiceResultModifyModel)
        {
            ProductsServicesResponse resultProduct = new ProductsServicesResponse();

            try
            {
                Products productUpdated = await _ProductsRepository.GetById(oProductServiceResultModifyModel.ProductId);

                if (productUpdated == null || productUpdated.Deleted == true)
                {
                    resultProduct.Message = "El producto no existe";
                    return resultProduct;
                }
                else
                {
                    if (await ValidateProduct(oProductServiceResultModifyModel.ProductName))
                    {

                        resultProduct.Message = $"Este producto '{oProductServiceResultModifyModel.ProductName}' ya esta registrado";
                        resultProduct.Success = false;
                    }
                    else
                    {
                        productUpdated.ProductName = oProductServiceResultModifyModel.ProductName;
                        productUpdated.SupplierId = oProductServiceResultModifyModel.SupplierId;
                        productUpdated.CategoryId = oProductServiceResultModifyModel.CategoryId;
                        productUpdated.UnitPrice = oProductServiceResultModifyModel.UnitPrice;
                        productUpdated.Discontinued = oProductServiceResultModifyModel.Discontinued;
                        productUpdated.UserMod = oProductServiceResultModifyModel.UserMod;
                        productUpdated.ModifyDate = oProductServiceResultModifyModel.ModifyDate;

                        _ProductsRepository.Update(productUpdated);
                        await _ProductsRepository.Commit();

                        resultProduct.Data = productUpdated;
                        resultProduct.Message = "Producto actualizado correctamente.";
                    }

                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error:{e.Message}");
                resultProduct.Success = false;
                resultProduct.Message = "Error agregando la informacion del producto";
            }
            return resultProduct;
        }
        public async Task<ProductsServicesResponse> RemoveProduct(int id)
        {
            ProductsServicesResponse productServiceResult = new ProductsServicesResponse();
            ProductsDeletedModel productDeleteModel = new ProductsDeletedModel();
            try
            {
                Products oProduct = await _ProductsRepository.GetById(id);

                if (oProduct == null || oProduct.Deleted == true)
                {
                    productServiceResult.Message = "El producto no existe";
                    return productServiceResult;
                }
                oProduct.Deleted = true;
                oProduct.UserDeleted = productDeleteModel.UserDeleted;
                oProduct.DeletedDate = productDeleteModel.DeletedDate;

                _ProductsRepository.Update(oProduct);
                await _ProductsRepository.Commit();

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
     
        
        /*
         Da problemas cuando hay un producto con una categoria eliminada o inexistente. Se agrego una validacion en el metodo de eliminar
           de categoria para que verifique si aun existe un producto con data de categoria antes de eliminar la misma. Pero esta correcto
            de esta forma?
         */
        public async Task<ProductsServicesResponse> GetProductById(int id)
        {
            ProductsServicesResponse productServiceResult = new ProductsServicesResponse();
            ProductsGetModel productGetModel = new ProductsGetModel();
            try
            {
                var oProduct = await _ProductsRepository.GetById(id);


                if (oProduct ==null || oProduct.Deleted == true)
                {
                    productServiceResult.Message = "El producto no existe";
                    productServiceResult.Data = null;
                    productServiceResult.Success = true;
                    return productServiceResult;
                }
                else
                {
                    var query = (from product in _ProductsRepository.FindAll().Where(c => c.ProductId == oProduct.ProductId)
                                 join category in _CategoriesRepository.FindAll().Where(c => c.CategoryId == oProduct.CategoryId)
                                 on product.CategoryId equals category.CategoryId
                                 join supplier in _SuppliersRepository.FindAll().Where(c => c.SupplierId == oProduct.SupplierId)
                                 on product.SupplierId equals supplier.SupplierId
                                 select new ProductsGetModel
                                 {
                                     ProductId = product.ProductId,
                                     ProductName = product.ProductName,
                                     UnitPrice = product.UnitPrice,
                                     Discontinued = product.Discontinued,
                                     CategoryName = category.CategoryName,
                                     CompanyName = supplier.CompanyName
                                 });
                    productServiceResult.Data = query;
                    productServiceResult.Message = "Producto encontrado";
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

            return await _ProductsRepository.Exists(Product => Product.ProductName == productName);

        }

    }
}