using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Sale.Api.Infrastructure.Services.Contracts;
using Shop.Sale.Api.Infrastructure.Services.Models.Orders;
using Shop.Shared.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersServices _IOrderServices;
        public OrdersController(IOrdersServices iOrdersServices)
        {
            this._IOrderServices = iOrdersServices;
        }

        [HttpGet]
        [EnableCors("AllowOrigin")]
        public ActionResult<ServicesResponses> Get()
        {
            return _IOrderServices.GetOrders();
        }

        [Route("{id:int}")]
        [HttpGet]
        public ActionResult<ServicesResponses> GetById(int id)
        {
            return _IOrderServices.GetOrderById(id);
        }

        [HttpPost]
        public async Task<ActionResult<ServicesResponses>> Save(OrdersAddModel ordersAddModel)
        {
            return await _IOrderServices.SaveOrder(ordersAddModel);
        }
        [HttpPut]
        public async Task<ActionResult<ServicesResponses>> Edit(OrdersEditModel ordersEditModel)
        {
            return await _IOrderServices.EditOrder(ordersEditModel);
        }

        [Route("{id:int}")]
        [HttpDelete]
        public async Task<ActionResult<ServicesResponses>> Delete(int id)
        {
            return await _IOrderServices.DeleteOrder(id);
        }
    }
}
