using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OpenSky.Converters;

namespace OpenSky.Entities
{
    /// <summary>
    /// Response for flight information
    /// </summary>
    [JsonConverter(typeof(InterfaceConverter<IOpenSkyFlight, OpenSkyFlight>))]
    public interface IOpenSkyFlight
    {
        /// <summary>
        /// Unique ICAO 24-bit transponder address for the aircraft
        /// </summary>
        [JsonProperty("icao24")]
        string Icao24 { get; }

        /// <summary>
        /// Estimated departure time for the flight
        /// </summary>
        [JsonConverter(typeof(UnixDateTimeConverter))]
        [JsonProperty("firstSeen")]
        DateTime FirstSeen { get; }

        /// <summary>
        /// ICAO code of the estimated departure airport. Null if the airport could not be identified
        /// </summary>
        [JsonProperty("estDepartureAirport")]
        string EstDepartureAirport { get; }

        /// <summary>
        /// Estimated arrival time for the flight
        /// </summary>
        [JsonConverter(typeof(UnixDateTimeConverter))]
        [JsonProperty("lastSeen")]
        DateTime LastSeen { get; }

        /// <summary>
        /// ICAO code of the estimated arrival airport. Null if the airport could not be identified
        /// </summary>
        [JsonProperty("estArrivalAirport")]
        string EstArrivalAirport { get; }

        /// <summary>
        /// Callsign of the aircraft. Null if no callsign has been received
        /// </summary>
        [JsonProperty("callSign")]
        string CallSign { get; }

        /// <summary>
        /// Horizontal distance of the last received aircraft position to the estimated departure airport, in meters
        /// </summary>
        [JsonProperty("estDepartureAirportHorizDistance")]
        int? EstDepartureAirportHorizDistance { get; }

        /// <summary>
        /// Vertical distance of the last received aircraft position to the estimated departure airport, in meters
        /// </summary>
        [JsonProperty("estDepartureAirportVertDistance")]
        int? EstDepartureAirportVertDistance { get; }

        /// <summary>
        /// Horizontal distance of the last received aircraft position to the estimated arrival airport, in meters
        /// </summary>
        [JsonProperty("estArrivalAirportHorizDistance")]
        int? EstArrivalAirportHorizDistance { get; }

        /// <summary>
        /// Vertical distance of the last received aircraft position to the estimated arrival airport, in meters
        /// </summary>
        [JsonProperty("estArrivalAirportVertDistance")]
        int? EstArrivalAirportVertDistance { get; }

        /// <summary>
        /// Number of other possible departure airports in short distance to estDepartureAirport
        /// </summary>
        [JsonProperty("departureAirportCandidatesCount")]
        int? DepartureAirportCandidatesCount { get; }

        /// <summary>
        /// Number of other possible arrival airports in short distance to estArrivalAirport
        /// </summary>
        [JsonProperty("arrivalAirportCandidatesCount")]
        int? ArrivalAirportCandidatesCount { get; }
    }

    public class OpenSkyFlight : IOpenSkyFlight
    {
        public string Icao24 { get; set; }
        public DateTime FirstSeen { get; set; }
        public string EstDepartureAirport { get; set; }
        public DateTime LastSeen { get; set; }
        public string EstArrivalAirport { get; set; }
        public string CallSign { get; set; }
        public int? EstDepartureAirportHorizDistance { get; set; }
        public int? EstDepartureAirportVertDistance { get; set; }
        public int? EstArrivalAirportHorizDistance { get; set; }
        public int? EstArrivalAirportVertDistance { get; set; }
        public int? DepartureAirportCandidatesCount { get; set; }
        public int? ArrivalAirportCandidatesCount { get; set; }
    }
}