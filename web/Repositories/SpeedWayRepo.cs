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

        //database functions
        public async Task SaveAsync(){
            await _db.SaveChangesAsync();
        }
        public SpeedWayRepo(Database db){
            _db = db;
        }
    }
}