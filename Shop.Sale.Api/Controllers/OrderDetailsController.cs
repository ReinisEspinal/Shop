using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Sale.Api.Infraestructure.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Shared.Core;

namespace Shop.Sale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailsService _IOrderDetailsService;
        public OrderDetailsController(IOrderDetailsService iOderDetailsService)
        {
            this._IOrderDetailsService = iOderDetailsService;
        }

        //Obtener lista de detalles de ordenes
        [HttpGet]
        public ActionResult<ServicesResponses> Get()
        {
            return _IOrderDetailsService.GetOrderDetails();
        }
    }
}
