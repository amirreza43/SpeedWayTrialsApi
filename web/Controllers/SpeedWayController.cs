using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;

namespace web
{
    [ApiController]
    [Route("api")]

    public class SpeedWayController : ControllerBase{
        private ISpeedWayRepo _repository;
        public SpeedWayController(ISpeedWayRepo repository)
        {
        _repository = repository;
        }
        //Driver functions
        [HttpPost("Drivers")]
        public async Task<IActionResult> AddDriver(DriverDto driverDto){
            Driver driver = new Driver(driverDto);
            await _repository.AddDriver(driver);
            await _repository.SaveAsync();
            return CreatedAtAction("GetDriver", new { driver.Id }, driver);
        }
        [HttpGet("Drivers/{id}")]
        public async Task<IActionResult> GetDriver(Guid Id){
            Console.WriteLine(Id);
            var driver = await _repository.GetDriver(Id);
            if(driver is null) return NotFound();
            return Ok(driver);
        }
        [HttpGet("Drivers/search")]
        public async Task<IActionResult> SearchDrivers([FromQuery] string nickname, [FromQuery] string firstName, [FromQuery] string lastName,
        [FromQuery] int age, [FromQuery] int wins, [FromQuery] int losses){
            IEnumerable<Driver> Drivers;
            if(nickname is not null)  Drivers = await _repository.GetDriversByNickName(nickname);
            if(firstName is not null)  Drivers = await _repository.GetDriversByFirstName(firstName);
            if(lastName is not null)  Drivers = await _repository.GetDriversByLastName(lastName);
            if(age is int a)  Drivers = await _repository.GetDriversByAge(age);
            if(wins is int w)  Drivers = await _repository.GetDriversByWins(wins);
            if(losses is int l)  Drivers = await _repository.GetDriversByLosses(losses);
            return Ok(Drivers);
        }
        [HttpGet("Drivers/all")]
        public async Task<IActionResult> GetAllDrivers(){
            return Ok(await _repository.GetAllDrivers());
        }
        //RaceCar functions
        [HttpPost("Drivers/{id}/RaceCar")]
        public async Task<IActionResult> AddRaceCar(Guid Id, RaceCarDto raceCarDto){
            var owner = await _repository.GetDriver(Id);
            if(owner is null) return NotFound();
            RaceCar raceCar = new RaceCar(raceCarDto){ DriverId = owner.Id};
            raceCar.Driver = owner;
            await _repository.AddRaceCar(raceCar, owner);
            await _repository.SaveAsync();
            return CreatedAtAction("GetRaceCar", new { raceCar.Id }, raceCar);
        }
        [HttpGet("RaceCar/{id}")]
        public async Task<IActionResult> GetRaceCar(Guid Id){
            Console.WriteLine(Id);
            var raceCar = await _repository.GetRaceCar(Id);
            if(raceCar is null) return NotFound();
            Console.WriteLine(raceCar.Driver.FirstName);
            return Ok(raceCar);
        }

        [HttpGet ("RaceCars/Nickname/{nickname}")]
        public async Task<IActionResult> GetRaceCarsByNickName(string nickname){
            var racecars=await _repository.GetRaceCarsByNickName(nickname);
            if(racecars.Count()==0) return NotFound();
            return Ok(racecars);
        }

        [HttpGet("RaceCars/Model/{model}")]
        public async Task<IActionResult> GetRaceCarsByModel(CarModels model){
            var racecars=await _repository.GetRaceCarsByModel(model);
            if(racecars.Count()==0) return NotFound();
            return Ok(racecars);
        }

        [HttpGet("RaceCars/Type/{type}")]
        public async Task<IActionResult> GetRaceCarsByType(CarTypes type){
            var racecars=await _repository.GetRaceCarsByCarType(type);
            if(racecars.Count()==0)return NotFound();
            return Ok(racecars);
        }
    }
}