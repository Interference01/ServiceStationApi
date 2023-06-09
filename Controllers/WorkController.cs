using Microsoft.AspNetCore.Mvc;
using ServiceStationApi.database;

namespace ServiceStationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkController : Controller
    {
        private readonly DbAutoContext dbContext;

        public WorkController(DbAutoContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetListWorks(int idAuto)
        {
            var car = await dbContext.Cars.FindAsync(idAuto);

            if (car == null)
                return NotFound("Works not found");
            //var listWorksDTO = car.


            return Ok();
        }
    }
}
