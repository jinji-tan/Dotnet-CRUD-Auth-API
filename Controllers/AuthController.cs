using System.Security.Cryptography;
using System.Text;
using DotnetCrudAuthApi.Dtos;
using DotnetCrudAuthApi.Repostories;
using DotnetCrudAuthApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCrudAuthApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService token;
        private readonly IAuthRepository auth;

        public AuthController(ITokenService token, IAuthRepository auth)
        {
            this.token = token;
            this.auth = auth;
        }

        [HttpGet]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            if (await auth.UserExists(userDto.Email))
                return BadRequest($"Email {userDto.Email} already exists");

            var success = await auth.Register(userDto);

            if (success == false)
                return BadRequest("Failed to Register");

            return Ok("Successfully Registered");
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDto userDto)
        {
            var user = await auth.GetUserByEmail(userDto.Email);

            if (user == null)
                return Unauthorized("Invalid Email or Password.");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password));

            if (!computedHash.SequenceEqual(user.PasswordHash))
                return BadRequest("Invalid Email or Password.");

            return Ok(new { token = this.token.CreateToken(userDto.Email) });
        }
    }
}