using Prodavnica.Api.Dto;
using Prodavnica.Api.Interfaces;

namespace Prodavnica.Api.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository _repository;

        public AdminService(IRepository repository)
        {
            _repository = repository;
        }

        public List<OrederDto> GetAllOrders()
        {
            return _repository.GetAllOrders();
        }

        public List<UserDto> GetAllUnverified()
        {
            return _repository.GetAllUnverified();
        }

        public bool Verify(string username, bool verify)
        {
            return _repository.Verify(username, verify);
        }
    }
}
