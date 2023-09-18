using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Prodavnica.Api.Constants;
using Prodavnica.Api.Dto;
using Prodavnica.Api.Interfaces;
using Prodavnica.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Prodavnica.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("RegisterUser")]
        public IActionResult RegisterUser([FromBody] UserDto userDto)
        {
            UserDto user;
            try
            {
                user = _service.RegisterUser(userDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            return Ok(user);
        }

        [HttpPut(Name = "ChangeProfile")]
        public IActionResult ChangeProfile(Guid id, [FromBody] UserDto userDto)
        {
            UserDto user;
            try
            {
                user = _service.ChangeProfile(id, userDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            return Ok(user);
        }

        [HttpPut]
        [Route("GetUser")]
        public IActionResult GetUser([FromQuery] string username)
        {
            UserDto user;
            try
            {
                user = _service.GetUser(username);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            string token = "";
            try
            {
                token = _service.Login(loginModel);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }

            return Ok(new { Token = token });
        }

        [HttpPut]
        [Route("GetType")]
        public IActionResult GetUserType(Guid id)
        {
            return Ok(_service.GetUserType(id));
        }

        [HttpGet]
        [Route("GetAllItems")]
        public IActionResult GetAllItems()
        {
            return Ok(_service.GetAllItems());
        }

        [HttpPost]
        [Route("MakeOrder")]
        public IActionResult MakeOrder([FromBody] OrederDto orederDto)
        {
            return Ok(_service.MakeOrder(orederDto));
        }

        [HttpGet]
        [Route("AllFinalizedPurchaces")]
        public IActionResult AllFinalizedPurchaces(Guid userId)
        {
            return Ok(_service.AllFinalizedPurchases(userId));
        }
    }
}
