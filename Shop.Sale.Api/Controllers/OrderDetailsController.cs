using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Sale.Api.Infrastructure.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Shared.Core;
using Shop.Sale.Api.Infrastructure.Services.Models.OrderDetails;

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

        [Route("Orders/{id:int}")]
        [HttpGet]
        public ActionResult<ServicesResponses> GetById(int id)
        {
            return _IOrderDetailsService.GetOrderDetailsById(id);
        }
        [Route("Orders/{id:int}/Products/{productid:int}")]
        [HttpGet]
        public ActionResult<ServicesResponses> GetById(int id,int productid)
        {
            return _IOrderDetailsService.GetOrderDetailsById(id,productid);
        }
        [HttpDelete]
        public async Task<ActionResult<ServicesResponses>> Delete(OrderDetailsDeleteModel orderDetailsDelete)
        {
            return await _IOrderDetailsService.DeleteOrderDetail(orderDetailsDelete);
        }

        [HttpPut]
        public async Task<ActionResult<ServicesResponses>> Update(OrderDetailsEditModel orderDetailsEdit)
        {
            return await _IOrderDetailsService.EditOrderDetails(orderDetailsEdit);
        }

        [HttpPost]
        public async Task<ActionResult<ServicesResponses>> Create(OrderDetailsAddModel newOrdeerDetails)
        {
            return await _IOrderDetailsService.SaveOrderDetails(newOrdeerDetails);
        }
    }
}
