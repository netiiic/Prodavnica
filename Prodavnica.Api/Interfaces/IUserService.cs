using Prodavnica.Api.Dto;
using Prodavnica.Api.Models;

namespace Prodavnica.Api.Interfaces
{
    public interface IUserService
    {
        UserDto RegisterUser(UserDto userDto);
        UserDto ChangeProfile(Guid id, UserDto userDto);
        UserDto GetUser(string username);
        string Login(LoginModel loginModel);
        int GetUserType(Guid id);
        List<ShoppingItemDto> GetAllItems();
        OrederDto MakeOrder(OrederDto order);
        List<OrederDto> AllFinalizedPurchases(Guid userId);
    }
}
