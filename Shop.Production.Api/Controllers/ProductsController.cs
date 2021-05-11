using Microsoft.AspNetCore.Mvc;
using System;
using Shop.Production.Api.Infrastructure.Services.Contracts;
using Shop.Shared.Core;
using Shop.Production.Api.Infrastructure.Services.Models;
using Shop.Production.Api.Infrastructure.Services.Core;
using Shop.Production.Api.Infrastructure.Services.Models.Product;
using System.Threading.Tasks;

namespace Shop.Production.Api.Data.Entities
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsServices _IProductService;
        public ProductsController(IProductsServices iProductService)
        {
            this._IProductService = iProductService;
        }

        [HttpGet]
        public ActionResult<ServicesResponses> Get()
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
        public async Task<ActionResult<ServicesResponses>> GetById(int id)
        {
            return await _IProductService.GetProductById(id);
        }

        [HttpPost]
        public async Task<ActionResult<ServicesResponses>> Create(ProductsAddModel productServiceResult)
        {
            return await _IProductService.SaveProduct(productServiceResult);
        }

        [HttpPut]
        public async Task<ActionResult<ServicesResponses>> Edit(ProductsModifyModel objectProductModify)
        {

            return await _IProductService.UpdateProduct(objectProductModify);
    
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<ActionResult<ServicesResponses>> Delete(int id)
        {
            return await _IProductService.RemoveProduct(id);
        }


    }
}
