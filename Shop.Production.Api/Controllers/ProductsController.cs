using Microsoft.AspNetCore.Mvc;
using System;
using Shop.Production.Api.Infrastructure.Services.Contracts;
using Shop.Shared.Core;
using Shop.Production.Api.Infrastructure.Services.ServicesResult.Models;
using Shop.Production.Api.Infrastructure.Services.ServicesResult.Core;
using Shop.Production.Api.Infrastructure.Services.ServicesResult.Models.Product;
using System.Threading.Tasks;

namespace Shop.Production.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _IProductService;
        public ProductsController(IProductService iProductService)
        {
            this._IProductService = iProductService;
        }

        [HttpGet]
        public ActionResult<ServiceReponse> Get()
        {
            try
            {
                return _IProductService.GetProducts();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        //Form <- Para decir la manera de mandar la informacion
        public async Task<ActionResult<ServiceReponse>> GetById(int id)
        {
            return await _IProductService.GetProductById(id);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceReponse>> Create(ProductServiceResultAddModel productServiceResult)
        {
            return await _IProductService.SaveProduct(productServiceResult);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceReponse>> Edit(ProductServiceResultModifyModel objectProductModify)
        {

            return await _IProductService.UpdateProduct(objectProductModify);
    
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<ActionResult<ServiceReponse>> Delete(int id)
        {
            return await _IProductService.RemoveProduct(id);
        }


    }
}
