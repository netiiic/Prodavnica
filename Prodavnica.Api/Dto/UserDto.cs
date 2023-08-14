namespace Prodavnica.Api.Dto
{
    public enum UserType { Byer, Seller, Admin }
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public UserType UserType { get; set; }
        public string Image { get; set; }
        public bool? Verified { get; set; }
    }
}
