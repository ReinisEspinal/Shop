using Microsoft.AspNetCore.Mvc;
using Shop.Production.Api.Infrastructure.Services.Contracts;
using Shop.Shared.Core;
using Shop.Production.Api.Infrastructure.Services.Core;
using System.Threading.Tasks;
using Shop.Production.Api.Infrastructure.Services.Models.Supplier;

namespace Shop.Production.Api.Data.Entities
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        public readonly ISuppliersServices _ISupplierService;
        public SuppliersController(ISuppliersServices iSupplierService)
        {
            this._ISupplierService = iSupplierService;
        }

        [HttpGet]
        public ActionResult<SuppliersServicesResponse> Get()
        {
            return _ISupplierService.GetSuppliers();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<ServicesResponses>> GetById(int id)
        {
            return await _ISupplierService.GetSupplierById(id);
        }

        [HttpPost]
        public async Task<ActionResult<ServicesResponses>> Create(SuppliersAddModel supplierAddModel)
        {
            return await _ISupplierService.SaveSupplier(supplierAddModel);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<ServicesResponses>> Delete(int id)
        {
            return await _ISupplierService.DeleteSupplier(id);
        }

        [HttpPut]
        public async Task<ActionResult<ServicesResponses>> Edit(SuppliersModifyModel supplierModifyModel)
        {
           
            return Ok(await _ISupplierService.UpdateSupplier(supplierModifyModel));
        }
    }
}