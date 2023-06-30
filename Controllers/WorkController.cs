using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceStationApi.database;
using ServiceStationApi.database.entities;
using ServiceStationApi.models;

namespace ServiceStationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarWorkController : Controller
    {
        private readonly DbAutoContext dbContext;

        public CarWorkController(DbAutoContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetListWorks(int idAuto)
        {
            var works = await dbContext.Cars.Include(o => o.CarWorks).FirstOrDefaultAsync(o => o.IdAuto == idAuto);

            if (works == null)
                return NotFound("Works not found");


            var carWorkDTO = works.CarWorks.ToList().Select(x => new CarWorkDTO()
            {
                IdAuto = x.IdAuto,
                IdWork = x.IdWork,
                DescriptionWork = x.DescriptionWork,
                Mileage = x.Mileage,
                Note = x.Note,
                Date = x.Date,
            });

            return Ok(carWorkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWork(int idAuto, [FromBody] CarWorkDTOPost carDTO)
        {
            var car = await dbContext.Cars.AnyAsync(x => x.IdAuto == idAuto);

            if (!car)
                return NotFound("Car not found");


            CarWork newCar = new CarWork()
            {
                IdAuto = idAuto,
                DescriptionWork = carDTO.Description,
                Mileage = carDTO.Mileage,
                Note = carDTO.Note,
                Date = HandlerDate.ConvertStrToDate(carDTO.Date)
            };

            await dbContext.CarWorks.AddAsync(newCar);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction("GetListWorks", new { newCar.IdAuto }, newCar);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCarWork(int idWork, [FromBody] CarWorkDTOPost updateCarWork)
        {
            var existingCarWork = await dbContext.CarWorks.FindAsync(idWork);

            if (existingCarWork == null)
                return NotFound("Car work not found");


            existingCarWork.DescriptionWork = updateCarWork.Description;
            existingCarWork.Date = HandlerDate.ConvertStrToDate(updateCarWork.Date);
            existingCarWork.Note = updateCarWork.Note;
            existingCarWork.Mileage = updateCarWork.Mileage;

            await dbContext.SaveChangesAsync();

            return Ok(existingCarWork);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCarWork(int idWork)
        {
            var deleteCarWork = await dbContext.CarWorks.FindAsync(idWork);

            if (deleteCarWork == null)
                return NotFound("Car work not found");


            dbContext.CarWorks.Remove(deleteCarWork);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
