using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Cors;

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
        [EnableCors("Policy1")]
        [HttpPost("Drivers")]
        public async Task<IActionResult> AddDriver(DriverDto driverDto){
            Driver driver = new Driver(driverDto);
            await _repository.AddDriver(driver);
            await _repository.SaveAsync();
            return CreatedAtAction("GetDriver", new { driver.Id }, driver);
        }
        [EnableCors("Policy1")]
        [HttpGet("Drivers/{id}")]
        public async Task<IActionResult> GetDriver(Guid Id){
            Console.WriteLine(Id);
            var driver = await _repository.GetDriver(Id);
            if(driver is null) return NotFound();
            return Ok(driver);
        }
        [EnableCors("Policy1")]
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
        [EnableCors("Policy1")]
        [HttpGet("Drivers/all")]
        public async Task<IActionResult> GetAllDrivers(){
            return Ok(await _repository.GetAllDrivers());
        }
        //more comments!
        //RaceCar functions
        [EnableCors("Policy1")]
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
        [EnableCors("Policy1")]
        [HttpGet("RaceCar/{id}")]
        public async Task<IActionResult> GetRaceCar(Guid Id){
            Console.WriteLine(Id);
            var raceCar = await _repository.GetRaceCar(Id);
            if(raceCar is null) return NotFound();
            Console.WriteLine(raceCar.Driver.FirstName);
            return Ok(raceCar);
        }
        [EnableCors("Policy1")]
        [HttpGet ("RaceCars/Nickname/{nickname}")]
        public async Task<IActionResult> GetRaceCarsByNickName(string nickname){
            var racecars=await _repository.GetRaceCarsByNickName(nickname);
            if(racecars.Count()==0) return NotFound();
            return Ok(racecars);
        }
        [EnableCors("Policy1")]
        [HttpGet("RaceCars/Model/{model}")]
        public async Task<IActionResult> GetRaceCarsByModel(CarModels model){
            var racecars=await _repository.GetRaceCarsByModel(model);
            if(racecars.Count()==0) return NotFound();
            return Ok(racecars);
        }
        [EnableCors("Policy1")]
        [HttpGet("RaceCars/Type/{type}")]
        public async Task<IActionResult> GetRaceCarsByType(CarTypes type){
            var racecars=await _repository.GetRaceCarsByCarType(type);
            if(racecars.Count()==0)return NotFound();
            return Ok(racecars);
        }
        [EnableCors("Policy1")]
        [HttpGet("RaceCars/Status/{status}")]
        public async Task<IActionResult> GetRaceCarsByStatus(Status status){
            var racecars=await _repository.GetRaceCarsByStatus(status);
            if(racecars.Count()==0)return NotFound();
            return Ok(racecars);
        }
        [EnableCors("Policy1")]
        [HttpGet("RaceCars/Year/{year}")]
        public async Task<IActionResult> GetRaceCarsByYear(int year){
            var racecars=await _repository.GetRaceCarsByYear(year);
            if(racecars.Count()==0)return NotFound();
            return Ok(racecars);
        }
        [EnableCors("Policy1")]
        [HttpGet("RaceCars")]
        public async Task<IActionResult> GetAllRaceCars(){
            var racecars=await _repository.GetAllRaceCars();
            if(racecars.Count()==0)return NotFound();
            return Ok(racecars);
        }

        [HttpPost("Race")]
        public async Task<IActionResult> AddRace(RaceDto racedto){
            Race race =new Race(racedto);
            await _repository.AddRace(race);
            await _repository.SaveAsync();
            return CreatedAtAction("GetRace", new { race.Id }, race);
        }

        [HttpGet ("Race/{id}")]
        public async Task<IActionResult> GetRace(Guid id){
            var race = await _repository.GetRace(id);
            foreach(var driver in race.Drivers){
                Console.WriteLine(driver.FirstName);
            }
            if(race is null) return NotFound();
            return Ok(race);
        }

        [HttpGet("Races/{name}")]
        public async Task<IActionResult> GetRacesByName(string name){
            var races=await _repository.GetRacesByName(name);
            if(races.Count()==0)return NotFound();
            return Ok(races);
        }

        [HttpGet("Races/date/{date}")]
        public async Task<IActionResult> GetRacesByDate(DateTime date){
            var races=await _repository.GetRacesByDate(date);
            if(races.Count()==0)return NotFound();
            return Ok(races);
        }

        [HttpGet("Races/category/{category}")]
        public async Task<IActionResult> GetRacesByCategory(RaceCategories category){
            var races=await _repository.GetRacesByRaceCategory(category);
            if(races.Count()==0)return NotFound();
            return Ok(races);
        }

        [HttpGet("Races")]
        public async Task<IActionResult> GetAllRaces(){
            var races=await _repository.GetAllRaces();
            if(races.Count()==0)return NotFound();
            return Ok(races);
        }

        [HttpPatch("Driver/{driverid}/Register/{raceid}")]
        public async Task<IActionResult> RegisterDriverForRace(Guid driverid, Guid raceid){
            var driver=await _repository.GetDriver(driverid);
            if(driver==null)return BadRequest("Driver does not exist");
            var race=await _repository.GetRace(raceid);
            if(race==null)return BadRequest("Race does not exist");
            _repository.RegisterDriverForRace(driver, race);
            await _repository.SaveAsync();
            return Ok();
        }

        [HttpGet("Race/{id}/Drivers")]
        public async Task<IActionResult> GetAllDriversRegisteredForARace(Guid id){
            var race=await _repository.GetRace(id);
            var drivers= _repository.GetAllDriversInRace(race);
            return Ok(drivers);
        }
    }
}