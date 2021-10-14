using System;
using Xunit;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using web;
using System.Threading.Tasks;

namespace test.Repositories
{
    public class SpeedWayRepoTest
    {
        private Database _db;
        private ISpeedWayRepo _repo;

        public SpeedWayRepoTest()
        {
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open();
            var options = new DbContextOptionsBuilder<Database>().UseSqlite(conn).Options;
            _db = new Database(options);
            _db.Database.EnsureDeleted();
            _db.Database.EnsureCreated();
            _repo = new SpeedWayRepo(_db);
        }
        //Driver function tests
        [Fact]
        public async Task ShouldSaveADriverToDatabase()
        {
            Driver driver = new Driver(){ FirstName = "John", LastName = "Doe", Age = 30, BirthDate = DateTime.Now };
            await _repo.AddDriver(driver);
            await _repo.SaveAsync();
            _db.Drivers.Count().Should().Be(1);
        }
        [Fact]
        public async Task GetDriverShouldReturnDriverFromDb(){
            Driver driver = new Driver(){ FirstName = "John", LastName = "Doe", Age = 30, BirthDate = DateTime.Now };
            await _repo.AddDriver(driver);
            await _repo.SaveAsync();
            var d = await _repo.GetDriver(driver.Id);
            d.FirstName.Should().Be("John");
        }
        [Fact]
        public async Task GetDriverByFirstNameShouldReturnDriversFromDb(){
            Driver driver = new Driver(){ FirstName = "John", LastName = "Doe", Age = 30, BirthDate = DateTime.Now };
            await _repo.AddDriver(driver);
            await _repo.SaveAsync();
            var d = await _repo.GetDriversByFirstName("John");
            d.Count().Should().Be(1);
        }
        [Fact]
        public async Task GetDriverByLastNameShouldReturnDriversFromDb(){
            Driver driver = new Driver(){ FirstName = "John", LastName = "Doe", Age = 30, BirthDate = DateTime.Now };
            await _repo.AddDriver(driver);
            await _repo.SaveAsync();
            var d = await _repo.GetDriversByLastName("Doe");
            d.Count().Should().Be(1);
        }
        [Fact]
        public async Task GetAllDriversShouldReturnDriversFromDb(){
            Driver driver = new Driver(){ FirstName = "John", LastName = "Doe", Age = 30, BirthDate = DateTime.Now };
            await _repo.AddDriver(driver);
            await _repo.SaveAsync();
            var d = await _repo.GetAllDrivers();
            d.Count().Should().Be(1);
        }
        //RaceCars Tests
        [Fact]
        public async Task ShouldSaveARaceCarToDatabase()
        {
            RaceCar car = new RaceCar(){ Nickname = "Johny", Year = 2018 , Model = CarModels.Maserati, Status = Status.AVAILABLE, CarType = CarTypes.sports };
            Driver driver = new Driver(){ FirstName = "John", LastName = "Doe", Age = 30, BirthDate = DateTime.Now };
            await _repo.AddRaceCar(car, driver);
            await _repo.SaveAsync();
            _db.RaceCars.Count().Should().Be(1);
        } 
    }
}