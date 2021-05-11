using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Sale.Api.Infrastructure.Services.Contracts;
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
        private readonly IOrderDetailsServices _IOrderDetailsService;
        public OrderDetailsController(IOrderDetailsServices iOderDetailsService)
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
