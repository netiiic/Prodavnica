using Prodavnica.Api.Dto;

namespace Prodavnica.Api.Interfaces
{
    public interface IAdminService
    {
        bool Verify(string username, bool verify);
        List<UserDto> GetAllUnverified();
        List<OrederDto> GetAllOrders();
    }
}
