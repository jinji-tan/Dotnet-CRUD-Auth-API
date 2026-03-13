using DotnetCrudAuthApi.Dtos;
using DotnetCrudAuthApi.Models;

namespace DotnetCrudAuthApi.Repostories
{
    public interface IAuthRepository
    {
        Task<bool> Register(UserDto userDto);
        Task<bool> UserExists(string email);
        Task<User?> GetUserByEmail(string email);
    }
}