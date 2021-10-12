using System;
using System.Collections.Generic;

namespace web
{
    public class RaceCar{
        public Guid Id {set; get;}
        public string Nickname {set; get;}
        public CarModels Model {set; get;}
        public int Year {set; get;}
        public int TopSpeed {set; get;}
        public Status Status {set; get;}
        public CarTypes CarType {set; get;}
        public Driver Driver {set; get;}
        public Guid DriverId {set; get;}
        public RaceCar(){
            Driver = new();
        }
        public RaceCar(RaceCarDto raceCarDto){
            Id = Guid.NewGuid();
            Nickname = raceCarDto.Nickname;
            Model = raceCarDto.Model;
            Year = raceCarDto.Year;
            TopSpeed = raceCarDto.TopSpeed;
            Status = raceCarDto.Status;
            CarType = raceCarDto.CarType;
            Driver = new();
        }
    }
}