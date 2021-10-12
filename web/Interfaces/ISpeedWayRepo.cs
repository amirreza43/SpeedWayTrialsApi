using System;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace web
{
    public interface ISpeedWayRepo{
        //Driver related functions
        Task AddDriver(Driver driver);
        Task<Driver> GetDriver(Guid Id);
        Task<IEnumerable<Driver>> GetDriversByNickName(string nickname);
        Task<IEnumerable<Driver>> GetDriversByFirstName(string firstName);
        Task<IEnumerable<Driver>> GetDriversByLastName(string lastName);
        Task<IEnumerable<Driver>> GetDriversByAge(int age);
        Task<IEnumerable<Driver>> GetDriversByWins(int wins);
        Task<IEnumerable<Driver>> GetDriversByLosses(int losses);
        Task<IEnumerable<Driver>> GetAllDrivers();
        //RaceCar related functions
        Task AddRaceCar(RaceCar raceCar, Driver driver);
        Task<RaceCar> GetRaceCar(Guid Id);
        // Task<IEnumerable<RaceCar>> GetRaceCarsByNickName(string nickname);
        // Task<IEnumerable<RaceCar>> GetRaceCarsByModel(CarModels model);
        // Task<IEnumerable<RaceCar>> GetRaceCarsByCarType(CarTypes type);
        // Task<IEnumerable<RaceCar>> GetRaceCarsByStatus(Status status);
        // Task<IEnumerable<RaceCar>> GetRaceCarsByYear(int year);
        // Task<IEnumerable<RaceCar>> GetAllRaceCars();
        // //Race related funtions
        // Task AddRace(RaceDto raceDto);
        // Task<Race> GetRace(Guid Id);
        // Task<IEnumerable<Race>> GetRacesByName(string name);
        // Task<IEnumerable<Race>> GetRacesByDate(DateTime date);
        // Task<IEnumerable<Race>> GetRacesByRaceCategorty(RaceCategories category);
        // Task<IEnumerable<Race>> GetAllParticipants(Race race);
        // Task<IEnumerable<Race>> GetAllRaces();
        Task SaveAsync();
    }
}