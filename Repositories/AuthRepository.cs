using System.Security.Cryptography;
using DotnetCrudAuthApi.Data;
using DotnetCrudAuthApi.Dtos;
using DotnetCrudAuthApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DotnetCrudAuthApi.Repostories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContextDapper dapper;
        public AuthRepository(DataContextDapper dapper)
        {
            this.dapper = dapper;
        }
        public async Task<bool> Register(UserDto userDto)
        {
            using var hmac = new HMACSHA512();

            byte[] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userDto.Password));
            byte[] passwordSalt = hmac.Key;

            string sql = @"INSERT INTO CrudSchema.Users (Email, PasswordHash, PasswordSalt) 
                        VALUES (@Email, @PasswordHash, @PasswordSalt)";

            return await dapper.Executesql(sql, new
            {
                Email = userDto.Email,
                passwordHash = passwordHash,
                passwordSalt = passwordSalt
            });
        }
        public async Task<bool> UserExists(string email)
        {
            string sql = @"SELECT * FROM CrudSchema.Users WHERE Email = @Email";
            var user = await dapper.LoadDataSingle<User>(sql, new { email });

            return user != null;
        }
        public async Task<User?> GetUserByEmail(string email)
        {
            string sql = @"SELECT * FROM CrudSchema.Users WHERE Email = @Email";
            return await dapper.LoadDataSingle<User>(sql, new { email });
        }
    }
}