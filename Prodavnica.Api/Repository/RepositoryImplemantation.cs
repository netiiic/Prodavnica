using AutoMapper;
using Prodavnica.Api.Dto;
using Prodavnica.Api.Infrastructure;
using Prodavnica.Api.Interfaces;
using Prodavnica.Api.Models;

namespace Prodavnica.Api.Repository
{
    public class RepositoryImplemantation : IRepository
    {
        private readonly IMapper _mapper;
        private readonly ProdavnicaDbContext _dbContext;

        public RepositoryImplemantation(IMapper mapper, ProdavnicaDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public UserDto ChangeProfile(Guid id, UserDto userDto)
        {
            User user = _dbContext.Users.Find(id);
            user.Username = userDto.Username;
            user.Email = userDto.Email;
            user.Password = userDto.Password;
            user.FullName = userDto.FullName;
            user.DateOfBirth = userDto.DateOfBirth;
            user.Address = userDto.Address;
            user.Image = userDto.Image;
            _dbContext.SaveChanges();

            return _mapper.Map<UserDto>(user);
        }

        public List<UserDto> GetAllUnverified()
        {
            List<User> users = _dbContext.Users.ToList();
            List<User> unverified = new();
            foreach (User user in users)
            {
                if (user.UserType == Models.UserType.Seller && user.Verified == false)
                {
                    unverified.Add(user);
                }
            }

            return _mapper.Map<List<UserDto>>(unverified);
        }

        public UserDto RegisterUser(UserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return _mapper.Map<UserDto>(userDto);
        }

        public UserDto UserLogin(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool Verify(Guid id, bool verify)
        {
            User user = _dbContext.Users.Find(id);
            if (user != null)
            {
                user.Verified = verify;
                _dbContext.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }

        }

        public bool UserExists(string username)
        {
            return _dbContext.Users.Any(c => c.Username == username);
        }

        public UserDto GetUser(string username)
        {
            User user = _dbContext.Users.SingleOrDefault(c => c.Username == username);
            return _mapper.Map<UserDto>(user);
        }

        public bool AuthenticateUser(string username, string password)
        {
            return _dbContext.Users.Any(c => c.Username == username && c.Password == password);
        }

        public int GetUserType(Guid id)
        {
            User user = _dbContext.Users.Find(id);
            return (int)user.UserType;
        }
    }
}
