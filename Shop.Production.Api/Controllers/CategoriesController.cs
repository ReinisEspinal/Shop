using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Production.Api.Infrastructure.Services.Contracts;
using Shop.Production.Api.Infrastructure.Services.ServicesResult.Core;
using Shop.Production.Api.Infrastructure.Services.ServicesResult.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Production.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _IcategoryService;

        public CategoriesController(ICategoryService iCategoryService)
        {
            this._IcategoryService = iCategoryService;
        }
        [HttpGet]
        public ActionResult<CategoryServiceResultCore> Get()
        {
            return  _IcategoryService.GetCategories();
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<CategoryServiceResultCore>> GetById(int id)
        {
            return await _IcategoryService.GetCategoryById(id);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryServiceResultCore>> SaveCategory(CategoryServiceResultAddModel categoriaAdd)
        {
            return await _IcategoryService.SaveCategory(categoriaAdd);
        }
        [HttpPut]
        public async Task<ActionResult<CategoryServiceResultCore>> EditCategory(CategoryServiceResultModifyModel categoryModifyModel)
        {
            return await _IcategoryService.UpdateCategory(categoryModifyModel);
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<ActionResult<CategoryServiceResultCore>> DeleteCategory(int id)
        {
            return await _IcategoryService.DeleteCategory(id);
        }
    }
}
