using Microsoft.AspNetCore.Mvc;
using System;
using Shop.Production.Api.Infrastructure.Services.Contracts;
using Shop.Shared.Core;
using Shop.Production.Api.Infrastructure.Services.ServicesResult.Models;
using Shop.Production.Api.Infrastructure.Services.ServicesResult.Core;

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

        [HttpPut]
        public ActionResult<ServiceReponse> Edit(ProductServiceResultModel objectProductModify)
        {
            ProductServiceResultCore productServiceResult = new ProductServiceResultCore();
            var product = new ProductServiceResultModel()
            {
                ProductId = objectProductModify.ProductId,
                ProductName = objectProductModify.ProductName,
                SupplierId = objectProductModify.SupplierId,
                CategoryId = objectProductModify.CategoryId,
                UnitPrice = objectProductModify.UnitPrice,
                Discontinued = objectProductModify.Discontinued
            };

            _IProductService.UpdateProduct(objectProductModify);

            productServiceResult.Success = true;
            productServiceResult.Message = "Producto editado";

            return productServiceResult;
        }
    }
}
