using Microsoft.AspNetCore.Mvc;
using System;
using VehicleTelemetryApi.Controllers;
using VehicleTelemetryApi.Models;
using Xunit;
using System.Linq;

namespace VehicleTelemetryApi.Tests
{
    public class TelemetryControllerTests
    {
        [Fact]
        public void Post_ValidData_ReturnsCreatedAtAction()
        {
            // Arrange
            var controller = new TelemetryController();
            var telemetryData = new TelemetryData
            {
                VehicleId = "Vehicle123",
                Timestamp = DateTime.UtcNow,
                Speed = 60,
                Latitude = 34.0522,
                Longitude = -118.2437,
                DiagnosticCode = "P0123"
            };

            // Act
            var result = controller.Post(telemetryData) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            Assert.Equal(nameof(TelemetryController.GetById), result.ActionName);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public void GetById_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var controller = new TelemetryController();
            var telemetryData = new TelemetryData
            {
                VehicleId = "Vehicle123",
                Timestamp = DateTime.UtcNow,
                Speed = 60,
                Latitude = 34.0522,
                Longitude = -118.2437,
                DiagnosticCode = "P0123"
            };
            var postResult = controller.Post(telemetryData) as CreatedAtActionResult;
            Guid id = ((TelemetryData)postResult.Value).Id;

            // Act
            var result = controller.GetById(id) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public void GetAll_ReturnsAllData()
        {
          var controller = new TelemetryController();
          var telemetryData1 = new TelemetryData
          {
            VehicleId = "Vehicle123",
            Timestamp = DateTime.UtcNow,
            Speed = 60,
            Latitude = 34.0522,
            Longitude = -118.2437,
            DiagnosticCode = "P0123"
          };

          var telemetryData2 = new TelemetryData
          {
            VehicleId = "Vehicle456",
            Timestamp = DateTime.UtcNow,
            Speed = 70,
            Latitude = 35.0522,
            Longitude = -119.2437,
            DiagnosticCode = "P0456"
          };

          controller.Post(telemetryData1);
          controller.Post(telemetryData2);

          //Act
          var result = controller.GetAll() as OkObjectResult;

          //Assert
          Assert.NotNull(result);
          Assert.Equal(200, result.StatusCode);
          Assert.NotNull(result.Value);
          Assert.Equal(2, ((System.Collections.Generic.List<TelemetryData>)result.Value).Count); //Verify count instead of specific data. Avoids test brittleness.
        }
    }
}
