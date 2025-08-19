using System;

namespace VehicleTelemetryApi.Models
{
    public class TelemetryData
    {
        public Guid Id { get; set; }
        public string VehicleId { get; set; }
        public DateTime Timestamp { get; set; }
        public double Speed { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string DiagnosticCode { get; set; }

        //Opportunity for refactoring:  Consider using a more specific data type for diagnostic codes (e.g., an enum) instead of a string.
    }
}
