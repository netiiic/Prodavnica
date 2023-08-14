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
        private readonly IRepository _repository;

        public UserController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("RegisterUser")]
        public IActionResult RegisterUser([FromBody] UserDto userDto)
        {
            if (_repository.UserExists(userDto.Username))
            {
                return Unauthorized("Username taken.");
            }
            userDto.Password = EncodePasswordToBase64(userDto.Password);

            return Ok(_repository.RegisterUser(userDto));
        }

        [HttpPut(Name = "ChangeProfile")]
        public IActionResult ChangeProfile(Guid id, [FromBody] UserDto userDto)
        {
            if (_repository.UserExists(userDto.Username))
            {
                return Unauthorized("Username taken.");
            }
            userDto.Password = EncodePasswordToBase64(userDto.Password);
            UserDto user = _repository.ChangeProfile(id, userDto);
            user.Password = DecodeFrom64(user.Password);
            return Ok(user);
        }

        [HttpPut]
        [Route("GetUser")]
        public IActionResult GetUser([FromQuery] string username)
        {
            if(!_repository.UserExists(username))
            {
                return Unauthorized("Non exist");
            }
            UserDto user = _repository.GetUser(username);
            user.Password = DecodeFrom64(user.Password);
            return Ok(user);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (!_repository.UserExists(loginModel.Username))
            {
                return Unauthorized("Username or password incorrect.");
            }

            UserDto user = _repository.GetUser(loginModel.Username);

            if (user.UserType == Dto.UserType.Seller && user.Verified == false)
            {
                return Unauthorized("Admin has not verifed you.");
            }
            string decPWD = string.Empty;

            decPWD = DecodeFrom64(user.Password);


            if (decPWD != loginModel.Password)
            {
                return Unauthorized("Username or password incorect.");
            }

            string token = CreateToken(user.Id.ToString(), user.Username, user.FullName);

            return Ok(new { Token = token });
        }

        [HttpPut]
        [Route("GetType")]
        public IActionResult GetUserType(Guid id)
        {
            return Ok(_repository.GetUserType(id));
        }

        [HttpGet]
        [Route("GetAllItems")]
        public IActionResult GetAllItems()
        {
            return Ok(_repository.GetAllItems());
        }

        [HttpPost]
        [Route("MakeOrder")]
        public IActionResult MakeOrder([FromBody] OrederDto orederDto)
        {
            return Ok(_repository.MakeOrder(orederDto));
        }

        private string CreateToken(string userId, string username, string fullName)
        {
            List<Claim> claims = new()
            {
                new Claim(CustomClaimTypes.UserId, userId),
                new Claim(CustomClaimTypes.Username, username),
                new Claim(CustomClaimTypes.FullName, fullName)
            };

            byte[] key = Encoding.ASCII.GetBytes("sdaspvsjmbvs9832kaedfgASF78979SDGVSDShsgsg");

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        private string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        private string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
    }
}
