using Dapper;
using DotnetCrudAuthApi.Data;
using DotnetCrudAuthApi.Dtos;
using DotnetCrudAuthApi.Models;

namespace DotnetCrudAuthApi.Repostories
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly DataContextDapper dapper;

        public PeopleRepository(DataContextDapper dapper)
        {
            this.dapper = dapper;
        }

        public async Task<IEnumerable<People>> GetAll<T>()
        {
            string sql = @"SELECT * FROM CrudSchema.People";

            return await dapper.LoadData<People>(sql);
        }
        public async Task<People?> GetById(int id)
        {
            string sql = @"SELECT * FROM CrudSchema.People WHERE Id = @id";

            return await dapper.LoadDataSingle<People>(sql, new { id });
        }
        public async Task<bool> Add(PeopleDto peopleDto)
        {
            string sql = @"INSERT INTO CrudSchema.People (FirstName, LastName, Age, Gender) 
                        VALUES (@FirstName, @LastName, @Age, @Gender)";

            return await dapper.Executesql(sql, peopleDto);
        }
        public async Task<bool> Update(int id, PeopleDto peopleDto)
        {
            string sql = @"UPDATE CrudSchema.People SET FirstName = @FirstName, LastName = @LastName, Age = @Age, Gender = @Gender WHERE Id = @Id";

            DynamicParameters parameters = new DynamicParameters(peopleDto);
            parameters.Add("Id", id);

            return await dapper.Executesql(sql, parameters);
        }
        public async Task<bool> Delete(int id)
        {
            string sql = @"DELETE FROM CrudSchema.People WHERE Id = @Id";

            return await dapper.Executesql(sql, new { Id = id });
        }
    }
}