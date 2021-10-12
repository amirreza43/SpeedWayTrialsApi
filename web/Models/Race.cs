using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace web
{
    public class Race{
        public Guid Id {set; get;}
        public string Name {set; get;}
        public DateTime Date {set; get;}
        public DateTime BestTime {set; get;}
        public string WinnerName {set; get;}
        [JsonIgnore]
        public List<Driver> Drivers {set; get;}
        [JsonIgnore]
        public List<DriverRace> DriverRace {set; get;}
        public RaceCategories RaceCategory {set; get;}
        public Race(){
            Drivers = new();
        }
        public Race(RaceDto raceDto){
            Id = Guid.NewGuid();
            Name = raceDto.Name;
            Date = raceDto.Date;
            BestTime = raceDto.BestTime;
            WinnerName = raceDto.WinnerName;
            RaceCategory = raceDto.RaceCategory;
            Drivers = new();
        }
    }
}