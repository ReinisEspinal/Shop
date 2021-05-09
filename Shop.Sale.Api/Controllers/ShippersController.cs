using Microsoft.AspNetCore.Mvc;
using Shop.Sale.Api.Infraestructure.Service.Contracts;
using Shop.Shared.Core;

namespace Shop.Sale.Api.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class ShippersController : Controller
    {
        private readonly IShipperService _IShipperService;
        public ShippersController(IShipperService iShipperService)
        {
            this._IShipperService = iShipperService;
        }
        //Get all Shippers
        [HttpGet]
        public ActionResult<ServiceReponse> Get()
        {
            return _IShipperService.GetShippers();
        }
    }
}
