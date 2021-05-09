using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shop.Production.Api.Infrastructure.Repository.Contracts;
using Shop.Production.Api.Infrastructure.Services.Contracts;
using Shop.Production.Api.Infrastructure.Services.ServicesResult.Core;
using Shop.Production.Api.Infrastructure.Services.ServicesResult.Models.Category;
using Shop.Production.Api.Infrastructure.Services.ServicesResult.Models.Supplier;
using System;
using System.Threading.Tasks;
using System.Linq;
namespace Shop.Production.Api.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _ICategoryRepository;
        private readonly ILogger<CategoryService> _Ilogger;
        private readonly IConfiguration _IConfiguration;
        private readonly IProductRepository _IProductRepository;

        public CategoryService(ICategoryRepository iCategoryRepository,
                                IProductRepository iProductRepository,
                               ILogger<CategoryService> iLogger,
                               IConfiguration iConfiguration)
        {
            this._ICategoryRepository = iCategoryRepository;
            this._Ilogger = iLogger;
            this._IConfiguration = iConfiguration;
            this._IProductRepository = iProductRepository;
        }
        public async Task<CategoryServiceResultCore> DeleteCategory(int id)
        {
            var categoryServiceResult = new CategoryServiceResultCore();
            var categoryDeleteModel = new CategoryServiceResultDeleteModel();

            try
            {
                var oCategory = await _ICategoryRepository.GetById(id);

                if (oCategory == null || oCategory.Deleted == true)
                {
                    categoryServiceResult.Success = false;
                    categoryServiceResult.Message = "La categoria no existe";
                }
                else
                {
                    if (_IProductRepository.FindAll().Where(c => c.CategoryId == id).Count() > 0)
                    {
                        categoryServiceResult.Success = false;
                        categoryServiceResult.Message = "Existen productos con la categoria asignada, quitar los productos con la categoria primero.";
                    }
                    else
                    {
                        oCategory.Deleted = categoryDeleteModel.Deleted;
                        oCategory.DeletedDate = categoryDeleteModel.DeletedDate;
                        oCategory.UserDeleted = categoryDeleteModel.UserDeleted;

                        _ICategoryRepository.Update(oCategory);
                        await _ICategoryRepository.Commit();
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
        public CategoryServiceResultCore GetCategories()
        {
            CategoryServiceResultCore categoryServiceResult = new CategoryServiceResultCore();

            try
            {
                var query = (from category in _ICategoryRepository.FindAll()
                             select new CategoryServiceResultGetModel
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
        public async Task<CategoryServiceResultCore> GetCategoryById(int id)
        {
            CategoryServiceResultCore categoryServiceResult = new CategoryServiceResultCore();
            var categoryServiceResultGetModel = new CategoryServiceResultGetModel();
            try
            {
                var x = await _ICategoryRepository.GetById(id);

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
        public async Task<CategoryServiceResultCore> SaveCategory(CategoryServiceResultAddModel category)
        {
            var categoryServiceResult = new CategoryServiceResultCore();
            try
            {
                if (await ValidateCategory(category.CategoryName) == true)
                {
                    await _ICategoryRepository.Add(new Data.Entities.Category()
                    {
                        CategoryName = category.CategoryName,
                        Description = category.Description,
                        CreationUser = category.CreationUser,
                        CreationDate = category.CreationDate
                    });

                    await _ICategoryRepository.Commit();
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
        public async Task<CategoryServiceResultCore> UpdateCategory(CategoryServiceResultModifyModel category)
        {
            var categoryServiceResult = new CategoryServiceResultCore();

            try
            {
                var oCategory = await _ICategoryRepository.GetById(category.CategoryId);

                if (await ValidateCategory(oCategory.CategoryName) == false)
                {
                    oCategory.CategoryName = category.CategoryName;
                    oCategory.Description = category.Description;
                    oCategory.UserMod = category.UserMod;
                    oCategory.ModifyDate = category.ModifyDate;

                    _ICategoryRepository.Update(oCategory);
                    categoryServiceResult.Success = true;
                    categoryServiceResult.Message = "Categoria editada.";

                    await _ICategoryRepository.Commit();
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
            return await _ICategoryRepository.Exists(category => category.CategoryName == categoryName);
        }
    }
}