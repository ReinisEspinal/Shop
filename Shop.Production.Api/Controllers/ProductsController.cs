using Microsoft.AspNetCore.Mvc;
using System;
using Shop.Production.Api.Infrastructure.Services.Contracts;
using Shop.Shared.Core;

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
    }
}
