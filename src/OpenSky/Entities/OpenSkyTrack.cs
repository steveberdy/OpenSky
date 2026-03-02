using System;

namespace OpenSky
{
    /// <summary>
    /// Aircraft Track
    /// </summary>
    public class OpenSkyTrack
    {
        /// <summary>
        /// Unique ICAO 24-bit transponder address for the aircraft
        /// </summary>
        public string Icao24 { get; set; }
        /// <summary>
        /// Time of the first waypoint
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Time of the last waypoint
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Callsign of the aircraft. Null if no callsign has been received
        /// </summary>
        public string CalllSign { get; set; }
        /// <summary>
        /// Waypoints of the trajectory
        /// </summary>
        public OpenSkyTrackPath[] Path { get; set; }
    }
}