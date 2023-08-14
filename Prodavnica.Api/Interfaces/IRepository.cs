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
        /// <param name="id"></param>
        /// <param name="verify"></param>
        /// <returns></returns>
        bool Verify(Guid id, bool verify);

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


        UserDto GetUser(string username);

        bool AuthenticateUser(string username, string password);

        int GetUserType(Guid id);
    }
}
