using Microsoft.AspNetCore.Mvc;
using Shop.Sale.Api.Infrastructure.Services.Contracts;
using Shop.Sale.Api.Infrastructure.Services.Models.Customers;
using Shop.Shared.Core;
using System.Threading.Tasks;

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

        [Route("{id:int}")]
        [HttpGet]
        public async Task<ActionResult<ServicesResponses>> GetById(int id)
        {
            return await _ICustomersService.GetCustomerById(id);
        }

        [HttpPost]
        public async Task<ActionResult<ServicesResponses>> Save(CustomersAddModel CustomersAddModel)
        {
            return await _ICustomersService.SaveCustomer(CustomersAddModel);
        }
        [HttpPut]
        public async Task<ActionResult<ServicesResponses>> Edit(CustomersModifyModel CustomersEditModel)
        {
            return await _ICustomersService.UpdateCustomer(CustomersEditModel);
        }

        [Route("{id:int}")]
        [HttpDelete]
        public async Task<ActionResult<ServicesResponses>> Delete(int id)
        {
            return await _ICustomersService.RemoveCustomer(id);
        }
    }
}
