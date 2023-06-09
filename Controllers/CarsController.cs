using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceStationApi.database;
using ServiceStationApi.database.entities;
using ServiceStationApi.models;

namespace ServiceStationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : Controller
    {
        private readonly DbAutoContext dbContext;

        public CarsController(DbAutoContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetCars(int idOwner)
        {
            var cars = await dbContext.Owners.Include(o => o.Cars).FirstOrDefaultAsync(o => o.IdUser == idOwner);

            if (cars == null)
                return NotFound("This owner has no registered cars");


            var carsDTO = cars.Cars.ToList().Select(x => new CarDTO()
            {
                idAuto = x.IdAuto,
                NameAuto = x.NameAuto,
                VINCode = x.VinCode,
                YearsOfManufacture = x.YearsOfManufacture
            });

            return Ok(carsDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(int idOwner,[FromBody] CarDTO carDTO)
        {
            var owner = await dbContext.Owners.AnyAsync(x => x.IdUser == idOwner);

            if (!owner)
                return NotFound("Owner not found");


            Car newCar = new Car()
            {
                IdUser = idOwner,
                NameAuto = carDTO.NameAuto,
                VinCode = carDTO.VINCode,
                YearsOfManufacture= carDTO.YearsOfManufacture
            };

            await dbContext.Cars.AddAsync(newCar);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction("GetCars", new { newCar.IdUser }, newCar);
        }

        [HttpPut]
        public async Task<IActionResult> updateCar(int idAuto, [FromBody] CarDTO updateCar)
        {
            var existingCar = await dbContext.Cars.FindAsync(idAuto);

            if (existingCar == null)
                return NotFound("Car not found");


            existingCar.NameAuto = updateCar.NameAuto;
            existingCar.YearsOfManufacture = updateCar.YearsOfManufacture;
            existingCar.VinCode = updateCar.VINCode;

            await dbContext.SaveChangesAsync();

            return Ok(existingCar);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCar(int idAuto)
        {
            var deleteCar = await dbContext.Cars.FindAsync(idAuto);

            if (deleteCar == null)
                return NotFound("Car not found");

            dbContext.Remove(deleteCar);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
