using System;
using Xunit;
using FluentAssertions;
using web;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace test.Models
{
  public class RaceCarTest
  {
    [Fact]
    public void ShouldCreateRaceCar()
    {
      RaceCar RaceCar = new RaceCar() { Nickname = "Johny", Year = 2018 , Model = CarModels.Maserati, Status = Status.AVAILABLE, CarType = CarTypes.sports };
      RaceCar.Nickname.Should().Be("Johny");
      RaceCar.Model.Should().Be(CarModels.Maserati);
    }
    [Fact]
    public void ShouldCreateRaceCarFromDto(){
        RaceCarDto RaceCarDto = new RaceCarDto(){ Nickname = "Johny", Year = 2018 , Model = CarModels.Maserati, Status = Status.AVAILABLE, CarType = CarTypes.sports };
        RaceCar RaceCar = new RaceCar(RaceCarDto);
        RaceCar.Nickname.Should().Be("Johny");
        RaceCar.Model.Should().Be(CarModels.Maserati);
    }
    [Fact]
    public void ShouldFailIfLessThanMinLength(){
        RaceCarDto RaceCarDto = new RaceCarDto(){ Nickname = "Jo", Year = 2018 , Model = CarModels.Maserati, Status = Status.AVAILABLE, CarType = CarTypes.sports };
        var context = new ValidationContext(RaceCarDto);
        Action act = () => Validator.ValidateObject(RaceCarDto, context, true);
        act.Should().Throw<ValidationException>().Where(e => e.Message.Contains("minimum length of '3'"));
    }
    [Fact]
    public void ShouldFailIfRequiredFieldNotEntered(){
        RaceCarDto RaceCarDto = new RaceCarDto(){ Year = 2018 , Model = CarModels.Maserati, Status = Status.AVAILABLE, CarType = CarTypes.sports };
        var context = new ValidationContext(RaceCarDto);
        Action act = () => Validator.ValidateObject(RaceCarDto, context, true);
        act.Should().Throw<ValidationException>().Where(e => e.Message.Contains("required"));
    }
  }
}