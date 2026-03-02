using System;
using System.Text.Json.Serialization;

namespace OpenSky
{
    /// <summary>
    /// State vector representing an aircraft's state
    /// </summary>
    [JsonConverter(typeof(OpenSkyStateVector))]
    public class OpenSkyStateVector
    {
        /// <summary>
        /// Unique ICAO 24-bit transponder address for the aircraft
        /// </summary>
        public string Icao24 { get; set; }

        /// <summary>
        /// Callsign of the aircraft. Null if no callsign has been received
        /// </summary>
        public string Callsign { get; set; }

        /// <summary>
        /// Country name inferred from the ICAO 24-bit address
        /// </summary>
        public string OriginCountry { get; set; }

        /// <summary>
        /// Time of the last position update. Can be null if no data was recently collected within the past few seconds
        /// </summary>
        public DateTime? TimePosition { get; set; }

        /// <summary>
        /// Time of general last update. This field is updated for any new message received from the transponder
        /// </summary>
        public int LastContact { get; set; }

        /// <summary>
        /// WGS-84 longitude in decimal degrees. Can be null
        /// </summary>
        public float? Longitude { get; set; }

        /// <summary>
        /// WGS-84 latitude in decimal degrees. Can be null
        /// </summary>
        public float? Latitude { get; set; }

        /// <summary>
        /// Barometric altitude in meters. Can be null
        /// </summary>
        public float? BaroAltitude { get; set; }

        /// <summary>
        /// Whether or no the received position is a surface position
        /// </summary>
        public bool OnGround { get; set; }

        /// <summary>
        /// Velocity over ground in meters per second. Can be null
        /// </summary>
        public float? Velocity { get; set; }

        /// <summary>
        /// Heading in decimal degrees clockwise from north. Can be null
        /// </summary>
        public float? TrueTrack { get; set; }

        /// <summary>
        /// Vertical rate in meters per second. Positive indicates ascent, negative indicates descent. Can be null
        /// </summary>
        public float? VerticalRate { get; set; }

        /// <summary>
        /// IDs of the receivers which contributed to this state vector. Can be null
        /// </summary>
        public int[] Sensors { get; set; }

        /// <summary>
        /// Geometric altitude in meters. Can be null
        /// </summary>
        public float? GeoAltitude { get; set; }

        /// <summary>
        /// The transponder code (Squawk) for the aircraft. Can be null
        /// </summary>
        public string Squawk { get; set; }

        /// <summary>
        /// Whether flight status indicates special purpose indicator
        /// </summary>
        public bool Spi { get; set; }

        /// <summary>
        /// Origin of this state’s position: 0 = ADS-B, 1 = ASTERIX, 2 = MLAT
        /// </summary>
        public int PositionSource { get; set; }
    }
}
