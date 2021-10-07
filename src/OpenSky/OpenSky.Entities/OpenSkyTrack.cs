using System;
using Newtonsoft.Json;
using OpenSky.Converters;

namespace OpenSky
{
    /// <summary>
    /// Aircraft Track
    /// </summary>
    [JsonConverter(typeof(InterfaceConverter<IOpenSkyTrack, OpenSkyTrack>))]
    internal interface IOpenSkyTrack
    {
        /// <summary>
        /// Unique ICAO 24-bit transponder address for the aircraft
        /// </summary>
        [JsonProperty("icao24")]
        string Icao24 { get; }

        /// <summary>
        /// Time of the first waypoint
        /// </summary>
        [JsonProperty("startTime")]
        DateTime StartTime { get; }

        /// <summary>
        /// Time of the last waypoint
        /// </summary>
        [JsonProperty("endTime")]
        DateTime EndTime { get; }

        /// <summary>
        /// Callsign of the aircraft. Null if no callsign has been received
        /// </summary>
        [JsonProperty("callSign")]
        string CalllSign { get; }

        /// <summary>
        /// Waypoints of the trajectory
        /// </summary>
        [JsonProperty("path")]
        OpenSkyTrackPath[] Path { get; }
    }

    public class OpenSkyTrack : IOpenSkyTrack
    {
        public string Icao24 { get; set; }
        [JsonConverter(typeof(FloatUnixTimeConverter))]
        public DateTime StartTime { get; set; }
        [JsonConverter(typeof(FloatUnixTimeConverter))]
        public DateTime EndTime { get; set; }
        public string CalllSign { get; set; }
        [JsonConverter(typeof(TrackPathArrayConverter))]
        public OpenSkyTrackPath[] Path { get; set; }
    }
}