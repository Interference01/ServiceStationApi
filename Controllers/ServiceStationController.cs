using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceStationApi.database;
using ServiceStationApi.database.entities;
using ServiceStationApi.models;

namespace ServiceStationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceStationController : Controller
    {
        private readonly DbAutoContext dbContext;

        public ServiceStationController(DbAutoContext dbContext)
        {
            this.dbContext = dbContext;  
        }



        [HttpGet("allOwners")]
        public async Task<IActionResult> GetAllOwners()
        {
            var list = await dbContext.Owners.ToListAsync();

            return Ok(list);
        }

        [HttpGet("searchOwner")]
        public async Task<IActionResult> SearchOwnersByName(string letters)
        {
            var owners = await dbContext.Owners.Where(x => x.NameOwner.StartsWith(letters)).ToListAsync();

            return Ok(owners);
        }

        [HttpPost("Owner")]
        public async Task<IActionResult> AddOwner([FromBody] OwnerDTO ownerDTO)
        {
            Owner owner = new Owner()
            {
                NameOwner = ownerDTO.NameOwner,
            };


            await dbContext.Owners.AddAsync(owner);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction("SearchOwnersByName", new { owner.NameOwner} , owner);
        }

        [HttpPut("Owner")]
        public async Task<IActionResult> UpdateOwner(int id, [FromBody] OwnerDTO updateOwner)
        {
            var existingOwner = await dbContext.Owners.FindAsync(id);

            if (existingOwner == null)
            {
                return NotFound();
            }

            existingOwner.NameOwner = updateOwner.NameOwner;
            existingOwner.RegistrationDate = updateOwner.RegistrationDate;

            await dbContext.SaveChangesAsync();

            return Ok(existingOwner);
        }

        [HttpDelete("Owner")]
        public async Task<IActionResult> DeleteOwner(int idOwner )
        {
            var deleteOwner = await dbContext.Owners.FindAsync(idOwner);

            if (deleteOwner == null)
            {
                return NotFound();
            }

            dbContext.Owners.Remove(deleteOwner);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}