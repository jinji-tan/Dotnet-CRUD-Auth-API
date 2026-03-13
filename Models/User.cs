namespace DotnetCrudAuthApi.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; } = "";
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];
    }
}