using System;
using Xunit;
using FluentAssertions;
using web;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace test.Models
{
  public class DriverTest
  {
    [Fact]
    public void ShouldCreateDriver()
    {
      Driver driver = new Driver() { FirstName = "John", LastName = "Doe", Age = 30, BirthDate = DateTime.Now };
      driver.FirstName.Should().Be("John");
      driver.LastName.Should().Be("Doe");
      driver.Age.Should().Be(30);
    }
    [Fact]
    public void ShouldCreateDriverFromDto(){
        DriverDto driverDto = new DriverDto(){ FirstName = "John", LastName = "Doe", Age = 30, BirthDate = DateTime.Now };
        Driver driver = new Driver(driverDto);
        driver.FirstName.Should().Be("John");
        driver.LastName.Should().Be("Doe");
        driver.Age.Should().Be(30);
    }
    [Fact]
    public void ShouldFailIfLessThanMinLength(){
        DriverDto driverDto = new DriverDto(){ FirstName = "Jo", LastName = "Doe", Age = 30, BirthDate = DateTime.Now };
        var context = new ValidationContext(driverDto);
        Action act = () => Validator.ValidateObject(driverDto, context, true);
        act.Should().Throw<ValidationException>().Where(e => e.Message.Contains("minimum length of '3'"));
    }
    [Fact]
    public void ShouldFailIfRequiredFieldNotEntered(){
        DriverDto driverDto = new DriverDto(){ LastName = "Doe", Age = 30, BirthDate = DateTime.Now };
        var context = new ValidationContext(driverDto);
        Action act = () => Validator.ValidateObject(driverDto, context, true);
        act.Should().Throw<ValidationException>().Where(e => e.Message.Contains("required"));
    }
  }
}
