using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shop.Production.Api.Infrastructure.Repository.Contracts;
using Shop.Production.Api.Infrastructure.Services.Contracts;
using Shop.Production.Api.Infrastructure.Services.Core;
using Shop.Production.Api.Infrastructure.Services.Models.Category;
using System;
using System.Threading.Tasks;
using System.Linq;
namespace Shop.Production.Api.Infrastructure.Services
{
    public class CategoriesServices : ICategoriesServices
    {
        private readonly ICategoriesRepository _ICategoriesRepository;
        private readonly ILogger<CategoriesServices> _Ilogger;
        private readonly IConfiguration _IConfiguration;
        private readonly IProductsRepository _IProductsRepository;

        public CategoriesServices(ICategoriesRepository iCategoriesRepository,
                                IProductsRepository iProductsRepository,
                               ILogger<CategoriesServices> iLogger,
                               IConfiguration iConfiguration)
        {
            this._ICategoriesRepository = iCategoriesRepository;
            this._Ilogger = iLogger;
            this._IConfiguration = iConfiguration;
            this._IProductsRepository = iProductsRepository;
        }
        public async Task<CategoriesServicesResponse> DeleteCategory(int id)
        {
            var categoryServiceResult = new CategoriesServicesResponse();
            var categoryDeleteModel = new CategoriesDeleteModel();

            try
            {
                var oCategory = await _ICategoriesRepository.GetById(id);

                if (oCategory == null || oCategory.Deleted == true)
                {
                    categoryServiceResult.Success = false;
                    categoryServiceResult.Message = "La categoria no existe";
                }
                else
                {
                    if (_IProductsRepository.FindAll().Where(c => c.CategoryId == id).Count() > 0)
                    {
                        categoryServiceResult.Success = false;
                        categoryServiceResult.Message = "Existen productos con la categoria asignada, quitar los productos con la categoria primero.";
                    }
                    else
                    {
                        oCategory.Deleted = categoryDeleteModel.Deleted;
                        oCategory.DeletedDate = categoryDeleteModel.DeletedDate;
                        oCategory.UserDeleted = categoryDeleteModel.UserDeleted;

                        _ICategoriesRepository.Update(oCategory);
                        await _ICategoriesRepository.Commit();
                        categoryServiceResult.Success = true;
                        categoryServiceResult.Message = "Categoria eliminada.";
                    }
                }

            }
            catch (Exception e)
            {
                _Ilogger.LogError($"Error eliminando la categoria.{e.Message}");
                categoryServiceResult.Message = "Error eliminando la categoria.";
                categoryServiceResult.Success = false;
            }
            return categoryServiceResult;
        }
        public CategoriesServicesResponse GetCategories()
        {
            CategoriesServicesResponse categoryServiceResult = new CategoriesServicesResponse();

            try
            {
                var query = (from category in _ICategoriesRepository.FindAll()
                             select new CategoriesGetModel
                             {
                                 CategoryName = category.CategoryName,
                                 Description = category.Description

                             }).ToList();

                categoryServiceResult.Data = query;
                categoryServiceResult.Success = true;
            }

            // categoryServiceResult.Message = "Lista de categorias."; No se envia, no es necesario

            catch (System.Exception e)
            {

                _Ilogger.LogError($"Error consultando las categorias.{e.Message}");
                categoryServiceResult.Message = "Error consultando las categoria.";
                categoryServiceResult.Success = false;
            }
            return categoryServiceResult;
        }
        public async Task<CategoriesServicesResponse> GetCategoryById(int id)
        {
            CategoriesServicesResponse categoryServiceResult = new CategoriesServicesResponse();
            var categoryServiceResultGetModel = new CategoriesGetModel();
            try
            {
                var x = await _ICategoriesRepository.GetById(id);

                if (x != null || x.Deleted == false)
                {
                    categoryServiceResultGetModel.CategoryName = x.CategoryName;
                    categoryServiceResultGetModel.Description = x.Description;

                    categoryServiceResult.Data = categoryServiceResultGetModel;
                    categoryServiceResult.Success = true;
                }
                else
                {
                    categoryServiceResult.Success = false;
                    categoryServiceResult.Message = "La categoria no existe";
                }
            }
            catch (Exception e)
            {
                _Ilogger.LogError($"Error filtrando por el numero de Id de la categoria {e.Message}");
                categoryServiceResult.Message = "Error filtrando por el numero de Id de la categoria";
                categoryServiceResult.Success = false;
            }
            return categoryServiceResult;
        }

        //Agregar validaciones
        //testing the new branch dev--S
        public async Task<CategoriesServicesResponse> SaveCategory(CategoriesAddModel category)
        {
            var categoryServiceResult = new CategoriesServicesResponse();
            try
            {
                if (await ValidateCategory(category.CategoryName) == true)
                {
                    await _ICategoriesRepository.Add(new Data.Entities.Categories()
                    {
                        CategoryName = category.CategoryName,
                        Description = category.Description,
                        CreationUser = category.CreationUser,
                        CreationDate = category.CreationDate
                    });

                    await _ICategoriesRepository.Commit();
                    categoryServiceResult.Success = true;
                    categoryServiceResult.Message = "Categoria insertada.";
                }
                categoryServiceResult.Success = false;
                categoryServiceResult.Message = "El nombre de categoria ya existe.";
            }
            catch (Exception e)
            {
                _Ilogger.LogError($"Error guardando la categoria. {e.Message}");
                categoryServiceResult.Message = "Error guardando la categoria.";
                categoryServiceResult.Success = false;
            }
            return categoryServiceResult;
        }

        //Agregar validaciones
        public async Task<CategoriesServicesResponse> UpdateCategory(CategoriesModifyModel category)
        {
            var categoryServiceResult = new CategoriesServicesResponse();

            try
            {
                var oCategory = await _ICategoriesRepository.GetById(category.CategoryId);

                if (await ValidateCategory(oCategory.CategoryName) == false)
                {
                    oCategory.CategoryName = category.CategoryName;
                    oCategory.Description = category.Description;
                    oCategory.UserMod = category.UserMod;
                    oCategory.ModifyDate = category.ModifyDate;

                    _ICategoriesRepository.Update(oCategory);
                    categoryServiceResult.Success = true;
                    categoryServiceResult.Message = "Categoria editada.";

                    await _ICategoriesRepository.Commit();
                    return categoryServiceResult;
                }
                categoryServiceResult.Success = false;
                categoryServiceResult.Message = "El nombre de categoria ya existe.";

            }
            catch (Exception e)
            {
                _Ilogger.LogError($"Error editando la categoria. {e.Message}");
                categoryServiceResult.Message = "Error editando la categoria.";
                categoryServiceResult.Success = false;
            }

            return categoryServiceResult;
        }

        public async Task<bool> ValidateCategory(string categoryName)
        {
            return await _ICategoriesRepository.Exists(category => category.CategoryName == categoryName);
        }
    }
}