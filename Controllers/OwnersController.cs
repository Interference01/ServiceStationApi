using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceStationApi.database;
using ServiceStationApi.database.entities;
using ServiceStationApi.models;

namespace ServiceStationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OwnersController : Controller
    {
        private readonly DbAutoContext dbContext;

        public OwnersController(DbAutoContext dbContext)
        {
            this.dbContext = dbContext;  
        }


        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllOwners()
        {
            var list = await dbContext.Owners.ToListAsync();

            return Ok(list);
        }

        [HttpGet("searchByName")]
        public async Task<IActionResult> SearchOwnersByName(string letters)
        {
            var owners = await dbContext.Owners.Where(x => x.NameOwner.StartsWith(letters)).ToListAsync();

            return Ok(owners);
        }

        [HttpPost]
        public async Task<IActionResult> AddOwner([FromBody] OwnerDTO ownerDTO)
        {
            Owner owner = new Owner()
            {
                NameOwner = ownerDTO.NameOwner,
                RegistrationDate = HandlerDate.ConvertStrToDate(ownerDTO.RegistrationDate)
            };


            await dbContext.Owners.AddAsync(owner);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction("SearchOwnersByName", new { owner.NameOwner} , owner);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOwner(int idOwner, [FromBody] OwnerDTO updateOwner)
        {
            var existingOwner = await dbContext.Owners.FindAsync(idOwner);

            if (existingOwner == null)
                return NotFound("Owner not found");


            existingOwner.NameOwner = updateOwner.NameOwner;
            existingOwner.RegistrationDate = HandlerDate.ConvertStrToDate(updateOwner.RegistrationDate);

            await dbContext.SaveChangesAsync();

            return Ok(existingOwner);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOwner(int idOwner )
        {
            var deleteOwner = await dbContext.Owners.FindAsync(idOwner);

            if (deleteOwner == null)
                return NotFound("Owner not found");
            

            dbContext.Owners.Remove(deleteOwner);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}