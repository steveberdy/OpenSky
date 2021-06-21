using System;
using Newtonsoft.Json;
using OpenSky.Converters;

namespace OpenSky.Entities
{
    /// <summary>
    /// State vector representing an aircraft's state
    /// </summary>
    public interface IOpenSkyStateVector
    {
        /// <summary>
        /// Unique ICAO 24-bit transponder address for the aircraft
        /// </summary>
        string Icao24 { get; }

        /// <summary>
        /// Callsign of the aircraft. Null if no callsign has been received
        /// </summary>
        string Callsign { get; }

        /// <summary>
        /// Country name inferred from the ICAO 24-bit address
        /// </summary>
        string OriginCountry { get; }

        /// <summary>
        /// Time of the last position update. Can be null if no data was recently collected within the past few seconds
        /// </summary>
        DateTime? TimePosition { get; }

        /// <summary>
        /// Time of general last update. This field is updated for any new message received from the transponder
        /// </summary>
        int LastContact { get; }

        /// <summary>
        /// WGS-84 longitude in decimal degrees. Can be null
        /// </summary>
        float? Longitude { get; }

        /// <summary>
        /// WGS-84 latitude in decimal degrees. Can be null
        /// </summary>
        float? Latitude { get; }

        /// <summary>
        /// Barometric altitude in meters. Can be null
        /// </summary>
        float? BaroAltitude { get; }

        /// <summary>
        /// Whether or no the received position is a surface position
        /// </summary>
        bool OnGround { get; }

        /// <summary>
        /// Velocity over ground in meters per second. Can be null
        /// </summary>
        float? Velocity { get; }

        /// <summary>
        /// Heading in decimal degrees clockwise from north. Can be null
        /// </summary>
        float? TrueTrack { get; }

        /// <summary>
        /// Vertical rate in meters per second. Positive indicates ascent, negative indicates descent. Can be null
        /// </summary>
        float? VerticalRate { get; }

        /// <summary>
        /// IDs of the receivers which contributed to this state vector. Can be null
        /// </summary>
        int[] Sensors { get; }

        /// <summary>
        /// Geometric altitude in meters. Can be null
        /// </summary>
        float? GeoAltitude { get; }

        /// <summary>
        /// The transponder code (Squawk) for the aircraft. Can be null
        /// </summary>
        string Squawk { get; }

        /// <summary>
        /// Whether flight status indicates special purpose indicator
        /// </summary>
        bool Spi { get; }

        /// <summary>
        /// Origin of this state’s position: 0 = ADS-B, 1 = ASTERIX, 2 = MLAT
        /// </summary>
        int PositionSource { get; }
    }

    [JsonConverter(typeof(StateVectorConverter))]
    public class OpenSkyStateVector : IOpenSkyStateVector
    {
        public string Icao24 { get; set; }
        public string Callsign { get; set; }
        public string OriginCountry { get; set; }
        public DateTime? TimePosition { get; set; }
        public int LastContact { get; set; }
        public float? Longitude { get; set; }
        public float? Latitude { get; set; }
        public float? GeoAltitude { get; set; }
        public bool OnGround { get; set; }
        public float? Velocity { get; set; }
        public float? Heading { get; set; }
        public float? VerticalRate { get; set; }
        public int[] Sensors { get; set; }
        public float? BaroAltitude { get; set; }
        public string Squawk { get; set; }
        public bool Spi { get; set; }
        public int PositionSource { get; set; }
        public float? TrueTrack { get; set; }
    }
}
