using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceStationApi.Models;

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



        [HttpGet]
        public async Task<IActionResult> GetAllOwners()
        {
            var list = await dbContext.Owners.ToListAsync();

            return Ok(list);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchOwnersByName(string letters)
        {
            var owners = await dbContext.Owners.Where(x => x.NameOwner.StartsWith(letters)).ToListAsync();

            return Ok(owners);
        }

        [HttpPost]
        public async Task<IActionResult> AddOwner(Owner owner)
        {
            await dbContext.Owners.AddAsync(owner);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction("SearchOnersByName",new { NameOwner = owner.NameOwner} , owner);
        }

    }
}