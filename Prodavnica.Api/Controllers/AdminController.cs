using Microsoft.AspNetCore.Mvc;
using Prodavnica.Api.Interfaces;

namespace Prodavnica.Api.Controllers
{
    namespace Prodavnica.Api.Controllers
    {
        [Route("api/admin")]
        [ApiController]
        public class AdminController : ControllerBase
        {
            private readonly IRepository _repository;

            public AdminController(IRepository repository)
            {
                _repository = repository;
            }

            [HttpPut]
            [Route("VerifyUser")]
            public IActionResult VerifyUser(string username, bool verify)
            {
                return Ok(_repository.Verify(username, verify));
            }

            [HttpGet]
            [Route("GetAllUnverified")]
            public IActionResult GetAllUnverified()
            {
                return Ok(_repository.GetAllUnverified());
            }

        }
    }
}
