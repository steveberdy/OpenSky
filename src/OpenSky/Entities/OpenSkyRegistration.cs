using System;

namespace OpenSky
{
    /// <summary>
    /// Data for an aircraft's registration information
    /// </summary>
    public class OpenSkyRegistration
    {
        public string Callsign { get; set; }
        public string ManufacturerName { get; set; }
        public string ManufacturerIcao { get; set; }
        public string Model { get; set; }
        public string Typecode { get; set; }
        public string SerialNumber { get; set; }
        public string LineNumber { get; set; }
        public string IcaoAircraftClass { get; set; }
        public string SelCal { get; set; }
        public string Operator { get; set; }
        public string OperatorCallsign { get; set; }
        public string OperatorIcao { get; set; }
        public string OperatorIata { get; set; }
        public string Owner { get; set; }
        public string CategoryDescription { get; set; }
        public DateTime? DateRegistered { get; set; }
        public DateTime? RegisteredUntil { get; set; }
        public string Status { get; set; }
        public DateTime? DateBuilt { get; set; }
        public DateTime? FirstFlightDate { get; set; }
        public string Engines { get; set; }
        public bool Modes { get; set; }
        public bool Adsb { get; set; }
        public bool Acars { get; set; }
        public bool Vdl { get; set; }
        public string Notes { get; set; }
        public string Country { get; set; }
        public DateTime? LastSeen { get; set; }
        public DateTime? FirstSeen { get; set; }
        public string Icao24 { get; set; }
        public DateTime Timestamp { get; set; }
    }
}