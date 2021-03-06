using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace web
{
    public class SpeedWayRepo : ISpeedWayRepo{
        private Database _db;
        //Driver functions
        public async Task AddDriver(Driver driver){
            await _db.AddAsync(driver);
        }
        public async Task<Driver> GetDriver(Guid Id){
            return await _db.Drivers.Where(d => d.Id == Id).Include(d => d.Cars).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Driver>> GetDriversByNickName(string nickname){
            return await _db.Drivers.Where(d => d.Nickname.Contains(nickname)).Include(d => d.Cars).ToListAsync();
        }
        public async Task<IEnumerable<Driver>> GetDriversByFirstName(string firstName){
            return await _db.Drivers.Where(d => d.FirstName.Contains(firstName)).Include(d => d.Cars).ToListAsync();
        }
        public async Task<IEnumerable<Driver>> GetDriversByLastName(string lastName){
            return await _db.Drivers.Where(d => d.LastName.Contains(lastName)).Include(d => d.Cars).ToListAsync();
        }
        public async Task<IEnumerable<Driver>> GetDriversByAge(int age){
            return await _db.Drivers.Where(d => d.Age == age).Include(d => d.Cars).ToListAsync();
        }
        public async Task<IEnumerable<Driver>> GetDriversByWins(int wins){
            return await _db.Drivers.Where(d => d.Wins >= wins).Include(d => d.Cars).ToListAsync();
        }
        public async Task<IEnumerable<Driver>> GetDriversByLosses(int losses){
            return await _db.Drivers.Where(d => d.Losses >= losses).Include(d => d.Cars).ToListAsync();
        }
        public async Task<IEnumerable<Driver>> GetAllDrivers(){
            return await _db.Drivers.Include(d => d.Cars).ToListAsync();
        }
        //RaceCar funtions
        public async Task AddRaceCar(RaceCar raceCar, Driver driver){
            driver.Cars.Add(raceCar);
            await _db.AddAsync(raceCar);
        }
        public async Task<RaceCar> GetRaceCar(Guid Id){
            var res = await _db.RaceCars.Where(r => r.Id == Id).Include(r => r.Driver).FirstAsync();
            Console.WriteLine($"Inside repo {res.DriverId}");
            var driver = await _db.Drivers.Where(d => d.Id == res.DriverId).FirstOrDefaultAsync();
            res.Driver = driver;
            return res;
        }

        public async Task<IEnumerable<RaceCar>> GetRaceCarsByNickName(string nickname){
            var racecars=await _db.RaceCars.Where(r=>r.Nickname.Contains(nickname)).Include(r=>r.Driver).ToListAsync();

            foreach(var racecar in racecars){

                var driver =await _db.Drivers.Where(d=>d.Id==racecar.DriverId).FirstOrDefaultAsync();
                racecar.Driver=driver;
            }
            
            return racecars;
        }

        public async Task<IEnumerable<RaceCar>> GetRaceCarsByModel(CarModels model){

            var racecars=await _db.RaceCars.Where(r=>r.Model==model).Include(r=>r.Driver).ToListAsync();

            foreach(var racecar in racecars){

                var driver =await _db.Drivers.Where(d=>d.Id==racecar.DriverId).FirstOrDefaultAsync();
                racecar.Driver=driver;
            }
            
            return racecars;
        }

        public async Task<IEnumerable<RaceCar>> GetRaceCarsByCarType(CarTypes type){
            var racecars=await _db.RaceCars.Where(r=>r.CarType==type).Include(r=>r.Driver).ToListAsync();

            foreach(var racecar in racecars){

                var driver =await _db.Drivers.Where(d=>d.Id==racecar.DriverId).FirstOrDefaultAsync();
                racecar.Driver=driver;
            }
            
            return racecars;
        }

        public async Task<IEnumerable<RaceCar>> GetRaceCarsByStatus(Status status){
            var racecars=await _db.RaceCars.Where(r=>r.Status==status).Include(r=>r.Driver).ToListAsync();

            foreach(var racecar in racecars){

                var driver =await _db.Drivers.Where(d=>d.Id==racecar.DriverId).FirstOrDefaultAsync();
                racecar.Driver=driver;
            }
            
            return racecars;
        }

        public async Task<IEnumerable<RaceCar>> GetRaceCarsByYear(int year){
            var racecars=await _db.RaceCars.Where(r=>r.Year==year).Include(r=>r.Driver).ToListAsync();

            foreach(var racecar in racecars){

                var driver =await _db.Drivers.Where(d=>d.Id==racecar.DriverId).FirstOrDefaultAsync();
                racecar.Driver=driver;
            }
            
            return racecars;
        }

        public async Task<IEnumerable<RaceCar>> GetAllRaceCars(){
            var racecars=await _db.RaceCars.Include(r=>r.Driver).ToListAsync();
            foreach(var racecar in racecars){

                var driver =await _db.Drivers.Where(d=>d.Id==racecar.DriverId).FirstOrDefaultAsync();
                racecar.Driver=driver;
            }
            return racecars;
        }

        //race functions
        public async Task AddRace(Race race){
            await _db.AddAsync(race);
        }

         public async Task<Race> GetRace(Guid Id){
            return await _db.Trials.Where(t=>t.Id==Id).Include(t=>t.Drivers).FirstOrDefaultAsync();
         }

         public async Task<IEnumerable<Race>> GetRacesByName(string name){
             return await _db.Trials.Where(t=>t.Name.Contains(name)).Include(t=>t.Drivers).ToListAsync();
         }

         public async Task<IEnumerable<Race>> GetRacesByDate(DateTime date){
             return await _db.Trials.Where(t=>t.Date==date).Include(t=>t.Drivers).ToListAsync();
         }

         public async Task<IEnumerable<Race>> GetRacesByRaceCategory(RaceCategories category){
             return await _db.Trials.Where(r=>r.RaceCategory==category).Include(r=>r.Drivers).ToListAsync();
         }

         public async Task<IEnumerable<Race>> GetAllRaces(){
             return await _db.Trials.Include(r=>r.Drivers).ToListAsync();
         }

         public void RegisterDriverForRace(Driver driver, Race race){
             driver.Races.Add(race);
             race.Drivers.Add(driver);
         }

        public IEnumerable<Driver> GetAllDriversInRace(Race race){
            return race.Drivers;
        }
        //database functions
        public async Task SaveAsync(){
            await _db.SaveChangesAsync();
        }
        public SpeedWayRepo(Database db){
            _db = db;
        }
    }
}