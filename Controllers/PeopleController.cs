using DotnetCrudAuthApi.Dtos;
using DotnetCrudAuthApi.Models;
using DotnetCrudAuthApi.Repostories;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCrudAuthApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleRepository peopleRepository;

        public PeopleController(IPeopleRepository peopleRepository)
        {
            this.peopleRepository = peopleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var people = await peopleRepository.GetAll();

            return Ok(people);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var person = await peopleRepository.GetById(id);
            if (person == null)
                return NotFound($"Person with id {id} not found");

            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PeopleDto peopleDto)
        {
            var success = await peopleRepository.Add(peopleDto);

            if (success == false)
                return BadRequest("Failed to Add");

            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PeopleDto peopleDto)
        {
            var success = await peopleRepository.Update(id, peopleDto);

            if (success == false)
                return BadRequest("Failed to Update");

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await peopleRepository.Delete(id);

            return Ok(success);
        }


    }
}