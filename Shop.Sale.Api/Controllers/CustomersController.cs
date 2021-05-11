using Microsoft.AspNetCore.Mvc;
using Shop.Sale.Api.Infrastructure.Services.Contracts;
using Shop.Shared.Core;

namespace Shop.Sale.Api.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly ICustomersServices _ICustomersService;
        public CustomersController(ICustomersServices iCustomersService)
        {
            this._ICustomersService= iCustomersService;
        }

        //Get all the customers
        [HttpGet]
        public ActionResult<ServicesResponses> Get()
        {
            return _ICustomersService.GetCustomers();
        }
    }
}
