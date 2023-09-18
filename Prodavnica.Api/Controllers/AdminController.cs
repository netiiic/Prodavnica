using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prodavnica.Api.Dto;
using Prodavnica.Api.Interfaces;

namespace Prodavnica.Api.Controllers
{
    namespace Prodavnica.Api.Controllers
    {
        [Route("api/admin")]
        [ApiController]
        public class AdminController : ControllerBase
        {
            private readonly IAdminService _service;

            public AdminController(IAdminService service)
            {
                _service = service;
            }

            [HttpPut]
            [Route("VerifyUser")]
            public IActionResult VerifyUser(string username, bool verify)
            {
                return Ok(_service.Verify(username, verify));
            }

            [HttpGet]
            [Route("GetAllUnverified")]
            public IActionResult GetAllUnverified()
            {
                return Ok(_service.GetAllUnverified());
            }

            [HttpGet]
            [Route("GetAllOrders")]
            public IActionResult GetAllOrders()
            {
                return Ok(_service.GetAllOrders());
            }

        }
    }
}
