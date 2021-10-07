using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OpenSky.Converters;

namespace OpenSky
{
    /// <summary>
    /// Data for an aircraft's registration information
    /// </summary>
    [JsonConverter(typeof(InterfaceConverter<IOpenSkyRegistration, OpenSkyRegistration>))]
    internal interface IOpenSkyRegistration
    {
        [JsonProperty("registration")]
        string Callsign { get; }

        [JsonProperty("manufacturerName")]
        string ManufacturerName { get; }

        [JsonProperty("manufacturerIcao")]
        string ManufacturerIcao { get; }

        [JsonProperty("model")]
        string Model { get; }

        [JsonProperty("typecode")]
        string Typecode { get; }

        [JsonProperty("serialNumber")]
        string SerialNumber { get; }

        [JsonProperty("lineNumber")]
        string LineNumber { get; }

        [JsonProperty("icaoAircraftClass")]
        string IcaoAircraftClass { get; }

        [JsonProperty("selCal")]
        string SelCal { get; }

        [JsonProperty("operator")]
        string Operator { get; }

        [JsonProperty("operatorCallsign")]
        string OperatorCallsign { get; }

        [JsonProperty("operatorIcao")]
        string OperatorIcao { get; }

        [JsonProperty("operatorIata")]
        string OperatorIata { get; }

        [JsonProperty("owner")]
        string Owner { get; }

        [JsonProperty("categoryDescription")]
        string CategoryDescription { get; }

        [JsonProperty("registered")]
        DateTime? DateRegistered { get; }

        [JsonProperty("regUntil")]
        DateTime? RegisteredUntil { get; }

        [JsonProperty("status")]
        string Status { get; }

        [JsonProperty("built")]
        DateTime? DateBuilt { get; }

        [JsonProperty("firstFlightDate")]
        DateTime? FirstFlightDate { get; }

        [JsonProperty("engines")]
        string Engines { get; }

        [JsonProperty("modes")]
        bool Modes { get; }

        [JsonProperty("adsb")]
        bool Adsb { get; }

        [JsonProperty("acars")]
        bool Acars { get; }

        [JsonProperty("vdl")]
        bool Vdl { get; }

        [JsonProperty("notes")]
        string Notes { get; }

        [JsonProperty("country")]
        string Country { get; }

        [JsonConverter(typeof(UnixDateTimeConverter)), JsonProperty("lastSeen")]
        DateTime? LastSeen { get; }

        [JsonConverter(typeof(UnixDateTimeConverter)), JsonProperty("firstSeen")]
        DateTime? FirstSeen { get; }

        [JsonProperty("icao24")]
        string Icao24 { get; }

        [JsonConverter(typeof(FloatUnixTimeConverter))]
        [JsonProperty("timestamp")]
        DateTime Timestamp { get; } // some provided aren't valid DateTimes
    }

    public class OpenSkyRegistration : IOpenSkyRegistration
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