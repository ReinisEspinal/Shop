using Microsoft.AspNetCore.Mvc;
using Shop.Sale.Api.Infrastructure.Services.Contracts;
using Shop.Shared.Core;

namespace Shop.Sale.Api.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class ShippersController : Controller
    {
        private readonly IShippersServices _IShippersService;
        public ShippersController(IShippersServices iShippersService)
        {
            this._IShippersService = iShippersService;
        }
        //Get all Shippers
        [HttpGet]
        public ActionResult<ServicesResponses> Get()
        {
            return _IShippersService.GetShippers();
        }
    }
}
