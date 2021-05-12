using Microsoft.AspNetCore.Mvc;
using Shop.Sale.Api.Infrastructure.Services.Contracts;
using Shop.Sale.Api.Infrastructure.Services.Models.Shippers;
using Shop.Shared.Core;
using System.Threading.Tasks;

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

        //Obtiene todo los Shippers
        [HttpGet]
        public ActionResult<ServicesResponses> Get()
        {
            return _IShippersService.GetShippers();
        }

        //Obtine shipper por ID
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<ServicesResponses> GetById(int id)
        {
            return _IShippersService.GetShipperById(id);
        }

        ///Agrega Shipper
        [HttpPost]
        public async Task<ActionResult<ServicesResponses>> SaveShipper(ShippersAddModel shipperAddModel)
        {
            return await _IShippersService.SaveShipper(shipperAddModel);
        }
        //Elimina Shipper
        [HttpDelete]
        public async Task<ActionResult<ServicesResponses>> DeleteShipper(ShippersDeleteModel shipperDeleteModel)
        {
            return await _IShippersService.DeleteShipper(shipperDeleteModel);
        }

        //Editar Shipper
        [HttpPut]
        public async Task<ActionResult<ServicesResponses>> Edit(ShippersEditModel shipperEditModel)
        {
            return await _IShippersService.EditShipper(shipperEditModel);
        }
    }
}
