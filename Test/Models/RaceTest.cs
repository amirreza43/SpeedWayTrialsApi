using System;
using Xunit;
using FluentAssertions;
using web;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace test.Models
{
  public class RaceTest
  {
    [Fact]
    public void ShouldCreateRace()
    {
      Race Race = new Race() { Name = "Speed Trials", Date = DateTime.Now, RaceCategory = RaceCategories.sportCar };
      Race.Name.Should().Be("Speed Trials");
      Race.RaceCategory.Should().Be(RaceCategories.sportCar);
    }
    [Fact]
    public void ShouldCreateRaceFromDto(){
        RaceDto RaceDto = new RaceDto(){ Name = "Speed Trials", Date = DateTime.Now, RaceCategory = RaceCategories.sportCar };
        Race Race = new Race(RaceDto);
        Race.Name.Should().Be("Speed Trials");
        Race.RaceCategory.Should().Be(RaceCategories.sportCar);
    }
    [Fact]
    public void ShouldFailIfLessThanMinLength(){
        RaceDto RaceDto = new RaceDto(){ Name = "Sp", Date = DateTime.Now, RaceCategory = RaceCategories.sportCar };
        var context = new ValidationContext(RaceDto);
        Action act = () => Validator.ValidateObject(RaceDto, context, true);
        act.Should().Throw<ValidationException>().Where(e => e.Message.Contains("minimum length of '3'"));
    }
    [Fact]
    public void ShouldFailIfRequiredFieldNotEntered(){
        RaceDto RaceDto = new RaceDto(){ Date = DateTime.Now, RaceCategory = RaceCategories.sportCar };
        var context = new ValidationContext(RaceDto);
        Action act = () => Validator.ValidateObject(RaceDto, context, true);
        act.Should().Throw<ValidationException>().Where(e => e.Message.Contains("required"));
    }
  }
}