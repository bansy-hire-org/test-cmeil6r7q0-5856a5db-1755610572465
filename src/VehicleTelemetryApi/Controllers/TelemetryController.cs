using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using VehicleTelemetryApi.Models;

namespace VehicleTelemetryApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TelemetryController : ControllerBase
    {
        private static readonly List<TelemetryData> _telemetryData = new List<TelemetryData>();

        [HttpPost]
        public IActionResult Post(TelemetryData data)
        {
            if (data == null || string.IsNullOrEmpty(data.VehicleId) || data.Timestamp == DateTime.MinValue)
            {
                return BadRequest("Invalid telemetry data.");
            }

            data.Id = Guid.NewGuid();
            _telemetryData.Add(data);

            return CreatedAtAction(nameof(GetById), new { id = data.Id }, data);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var data = _telemetryData.FirstOrDefault(t => t.Id == id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_telemetryData);
        }

        // Potential bug: Data is not thread-safe. In a real-world scenario, concurrent requests could lead to data corruption.  Candidate should identify this and suggest using a concurrent data structure or a database.
    }
}
