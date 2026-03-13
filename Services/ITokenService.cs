namespace DotnetCrudAuthApi.Services
{
    public interface ITokenService
    {
        string CreateToken(string email);
    }
}