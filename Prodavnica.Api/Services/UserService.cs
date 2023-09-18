using Microsoft.IdentityModel.Tokens;
using Prodavnica.Api.Constants;
using Prodavnica.Api.Dto;
using Prodavnica.Api.Interfaces;
using Prodavnica.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Prodavnica.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;
        private readonly IConfiguration _configuration;

        public UserService(IRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }


        public UserDto RegisterUser(UserDto userDto)
        {
            if (_repository.UserExistsEmail(userDto.Email))
            {
                throw new Exception("Email taken.");
            }
            userDto.Password = EncodePasswordToBase64(userDto.Password);

            return _repository.RegisterUser(userDto);
        }

        public UserDto ChangeProfile(Guid id, UserDto userDto)
        {
            userDto.Password = EncodePasswordToBase64(userDto.Password);
            UserDto user = _repository.ChangeProfile(id, userDto);
            user.Password = DecodeFrom64(user.Password);
            return user;
        }

        public UserDto GetUser(string username)
        {
            if (!_repository.UserExists(username))
            {
                throw new Exception("Doesn't exist");
            }
            UserDto user = _repository.GetUser(username);
            user.Password = DecodeFrom64(user.Password);
            return user;
        }

        public string Login(LoginModel loginModel)
        {
            if (!_repository.UserExistsEmail(loginModel.Email))
            {
                throw new Exception("Email or password incorrect.");
            }

            UserDto user = _repository.GetUserEmail(loginModel.Email);

            if (user.UserType == Dto.UserType.Seller && user.Verified == false)
            {
                throw new Exception("Admin has not verifed you.");
            }
            string decPWD = string.Empty;

            decPWD = DecodeFrom64(user.Password);


            if (decPWD != loginModel.Password)
            {
                throw new Exception("Username or password incorect.");
            }

            string token = CreateToken(user.Id.ToString(), user.Username, user.FullName);

            return  token;
        }

        public int GetUserType(Guid id)
        {
            return _repository.GetUserType(id);
        }

        public List<ShoppingItemDto> GetAllItems()
        {
            return _repository.GetAllItems();
        }

        public OrederDto MakeOrder(OrederDto order)
        {
            return _repository.MakeOrder(order);
        }

        public List<OrederDto> AllFinalizedPurchases(Guid userId)
        {
            return _repository.UserFinalizedPurchases(userId);
        }
        private string CreateToken(string userId, string username, string fullName)
        {
            List<Claim> claims = new()
            {
                new Claim(CustomClaimTypes.UserId, userId),
                new Claim(CustomClaimTypes.Username, username),
                new Claim(CustomClaimTypes.FullName, fullName)
            };

            byte[] key = Encoding.ASCII.GetBytes(_configuration["JWT:Key"]);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
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
