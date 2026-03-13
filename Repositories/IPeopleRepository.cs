using DotnetCrudAuthApi.Dtos;
using DotnetCrudAuthApi.Models;

namespace DotnetCrudAuthApi.Repostories
{
    public interface IPeopleRepository
    {
        Task<IEnumerable<People>> GetAll();
        Task<People?> GetById(int id);
        Task<bool> Add(PeopleDto peopleDto);
        Task<bool> Update(int id, PeopleDto peopleDto);
        Task<bool> Delete(int id);
    }
}