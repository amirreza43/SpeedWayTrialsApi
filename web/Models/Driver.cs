using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace web
{
    public class Driver{
        public Guid Id {set; get;}
        public string FirstName {set; get;}
        public string LastName {set; get;}
        public string Nickname {set; get;}
        public int Age {set; get;}
        public int Wins {set; get;}
        public int Losses {set; get;}
        public DateTime BirthDate {set; get;}
        [JsonIgnore]
        public List<RaceCar> Cars {set; get;}
        [JsonIgnore]
        public List<Race> Races {get; set;}
        [JsonIgnore]
        public List<DriverRace> DriverRace {set; get;}
        public Driver(){
            Cars = new();
            Races = new();
        }
        public Driver(DriverDto driverDto){
            Id = Guid.NewGuid();
            FirstName = driverDto.FirstName;
            LastName = driverDto.LastName;
            Nickname = driverDto.Nickname;
            Age = driverDto.Age;
            Wins = driverDto.Wins;
            Losses = driverDto.Losses;
            BirthDate = driverDto.BirthDate;
            Cars = new();
            Races = new();
        }
    }
}