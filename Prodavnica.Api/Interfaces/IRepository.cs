using Prodavnica.Api.Dto;

namespace Prodavnica.Api.Interfaces
{
    public interface IRepository
    {
        /// <summary>
        ///     Registers a new user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        UserDto RegisterUser(UserDto userDto);

        /// <summary>
        ///     Update user profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userDto"></param>
        /// <returns></returns>
        UserDto ChangeProfile(Guid id, UserDto userDto);

        /// <summary>
        ///     Verify a new seller
        /// </summary>
        /// <param name="username"></param>
        /// <param name="verify"></param>
        /// <returns></returns>
        bool Verify(string username, bool verify);

        /// <summary>
        ///     Get all unverified sellers
        /// </summary>
        /// <returns></returns>
        List<UserDto> GetAllUnverified();

        /// <summary>
        ///     Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserDto UserLogin(string username, string password);

        /// <summary>
        ///     Check if user exists in DB
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        bool UserExists(string username);

        /// <summary>
        ///     Check if user exists in DB by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool UserExistsByGuid(Guid id);


        UserDto GetUser(string username);

        bool AuthenticateUser(string username, string password);

        int GetUserType(Guid id);

        /// <summary>
        ///     Add new item to DB
        /// </summary>
        /// <param name="newItem"></param>
        /// <returns></returns>
        ShoppingItemDto AddNewItemToDB(ShoppingItemDto newItem);

        /// <summary>
        ///     Get all items from one seller
        /// </summary>
        /// <param name="sellerId"></param>
        /// <returns></returns>
        List<ShoppingItemDto> GetSellerItems(Guid sellerId);

        /// <summary>
        ///     Update existing item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shoppingItem"></param>
        /// <returns></returns>
        ShoppingItemDto UpdateItem(Guid id, ShoppingItemDto shoppingItem);

        /// <summary>
        ///     Delete shoppingItem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteItem(Guid id);

        /// <summary>
        ///     Get all items
        /// </summary>
        /// <returns></returns>
        List<ShoppingItemDto> GetAllItems();
    }
}
