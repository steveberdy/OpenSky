using System;

namespace OpenSky
{
    /// <summary>
    /// Response for flight information
    /// </summary>
    public class OpenSkyFlight
    {
        /// <summary>
        /// Unique ICAO 24-bit transponder address for the aircraft
        /// </summary>
        public string Icao24 { get; set; }
        /// <summary>
        /// Estimated departure time for the flight
        /// </summary>
        public DateTime FirstSeen { get; set; }
        /// <summary>
        /// ICAO code of the estimated departure airport. Null if the airport could not be identified
        /// </summary>
        public string EstDepartureAirport { get; set; }
        /// <summary>
        /// Time of last update from aircraft
        /// </summary>
        public DateTime LastSeen { get; set; }
        /// <summary>
        /// ICAO code of the estimated arrival airport. Null if the airport could not be identified
        /// </summary>
        public string EstArrivalAirport { get; set; }
        /// <summary>
        /// Callsign of the aircraft. Null if no callsign has been received
        /// </summary>
        public string CallSign { get; set; }
        /// <summary>
        /// Horizontal distance of the last received aircraft position to the estimated departure airport, in meters
        /// </summary>
        public int? EstDepartureAirportHorizDistance { get; set; }
        /// <summary>
        /// Vertical distance of the last received aircraft position to the estimated departure airport, in meters
        /// </summary>
        public int? EstDepartureAirportVertDistance { get; set; }
        /// <summary>
        /// Horizontal distance of the last received aircraft position to the estimated arrival airport, in meters
        /// </summary>
        public int? EstArrivalAirportHorizDistance { get; set; }
        /// <summary>
        /// Vertical distance of the last received aircraft position to the estimated arrival airport, in meters
        /// </summary>
        public int? EstArrivalAirportVertDistance { get; set; }
        /// <summary>
        /// Number of other possible departure airports in short distance to estDepartureAirport
        /// </summary>
        public int? DepartureAirportCandidatesCount { get; set; }
        /// <summary>
        /// Number of other possible arrival airports in short distance to estArrivalAirport
        /// </summary>
        public int? ArrivalAirportCandidatesCount { get; set; }
    }
}