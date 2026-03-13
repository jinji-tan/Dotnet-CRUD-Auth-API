using System.Data.Common;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DotnetCrudAuthApi.Data
{
    public class DataContextDapper
    {
        IConfiguration config;
        public DataContextDapper(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<IEnumerable<T>> LoadData<T>(string sql)
        {
            using DbConnection connection = new SqlConnection(config.GetConnectionString("DefaultConnection"));

            return await connection.QueryAsync<T>(sql);
        }

        public async Task<T?> LoadDataSingle<T>(string sql, object? parameters = null)
        {
            using DbConnection connection = new SqlConnection(config.GetConnectionString("DefaultConnection"));

            return await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
        }

        public async Task<bool> Executesql(string sql, object? parameter = null)
        {
            using DbConnection connection = new SqlConnection(config.GetConnectionString("DefaultConnection"));

            return await connection.ExecuteAsync(sql, parameter) > 0;
        }
    }
}