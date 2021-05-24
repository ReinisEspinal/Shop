using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Shop.Security.Api.Infrastructure.Context;
using Shop.Security.Api.Models;


namespace Shop.Security.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {

        private readonly SecurityContext securityContext;

        public UsersController(SecurityContext securityContext)
        {
            this.securityContext = securityContext;
        }
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var users = this.securityContext.Users.Select(us => new UserListModel(us)).ToArray();

            return Ok(users);
        }
    }
}