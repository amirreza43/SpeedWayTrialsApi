using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using web;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace test.Controllers
{
    public class SpeedWayControllerTest
    {
        private SpeedWayController _controller;
        private Mock<ISpeedWayRepo> _mockRepository;
        private DefaultHttpContext _httpContext;
        public SpeedWayControllerTest()
        {
            _mockRepository = new Mock<ISpeedWayRepo>();
            _httpContext = new DefaultHttpContext();
            _controller = new SpeedWayController(_mockRepository.Object)
            { ControllerContext = new ControllerContext() { HttpContext = _httpContext } };
        }
        [Fact]
        public async Task ShouldCreateDriver()
        {
            var dto = new DriverDto() { FirstName = "John", LastName = "Doe", Age = 30, BirthDate = DateTime.Now };
            var result = await _controller.AddDriver(dto);
            var createdActionResult = result as CreatedAtActionResult;
            createdActionResult.StatusCode.Should().Be(201);
            createdActionResult.ActionName.Should().Be("GetDriver");
            createdActionResult.RouteValues["Id"].Should().NotBeNull();
        }
        [Fact]
        public async Task ShouldGetADriver()
        {
            Guid guid = Guid.NewGuid();
            _mockRepository.Setup(obj => obj.GetDriver(guid)).Returns(Task.FromResult(
            new Driver() { Id = guid,
            FirstName = "John",
            LastName= "Doe",
            Nickname= "Johny",
            Age= 0,
            Wins= 0,
            Losses= 0,
            BirthDate= DateTime.Now }));
            var result = await _controller.GetDriver(guid);
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            (okResult.Value as Driver).FirstName.Should().Be("John");
            _mockRepository.Verify(obj => obj.GetDriver(guid));
        }
        [Fact]
        public async Task ShouldSearchAllDrivers(){
            var result = await _controller.SearchDrivers("Johny", "", "", 0, 0, 0);
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task ShouldGetAllDrivers(){
            var result = await _controller.GetAllDrivers();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
        }
    }
}