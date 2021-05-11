using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Production.Api.Infrastructure.Services.Contracts;
using Shop.Production.Api.Infrastructure.Services.Core;
using Shop.Production.Api.Infrastructure.Services.Models.Category;
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
        private readonly ICategoriesServices _IcategoryService;

        public CategoriesController(ICategoriesServices iCategoryService)
        {
            this._IcategoryService = iCategoryService;
        }
        [HttpGet]
        public ActionResult<CategoriesServicesResponse> Get()
        {
            return  _IcategoryService.GetCategories();
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<CategoriesServicesResponse>> GetById(int id)
        {
            return await _IcategoryService.GetCategoryById(id);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriesServicesResponse>> SaveCategory(CategoriesAddModel categoriaAdd)
        {
            return await _IcategoryService.SaveCategory(categoriaAdd);
        }
        [HttpPut]
        public async Task<ActionResult<CategoriesServicesResponse>> EditCategory(CategoriesModifyModel categoryModifyModel)
        {
            return await _IcategoryService.UpdateCategory(categoryModifyModel);
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<ActionResult<CategoriesServicesResponse>> DeleteCategory(int id)
        {
            return await _IcategoryService.DeleteCategory(id);
        }
    }
}
