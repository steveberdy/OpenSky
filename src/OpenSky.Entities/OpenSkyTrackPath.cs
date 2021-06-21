using System;
using Newtonsoft.Json;
using OpenSky.Converters;

namespace OpenSky.Entities
{
    /// <summary>
    /// Track Path
    /// </summary>
    public interface IOpenSkyTrackPath
    {
        /// <summary>
        /// Time of the given waypoint
        /// </summary>
        DateTime Time { get; }

        /// <summary>
        /// WGS-84 latitude in decimal degrees. Can be null
        /// </summary>
        float? Latitude { get; }

        /// <summary>
        /// WGS-84 longitude in decimal degrees. Can be null
        /// </summary>
        float? Longitude { get; }

        /// <summary>
        /// Barometric altitude in meters. Can be null
        /// </summary>
        float? BaroAltitude { get; }

        /// <summary>
        /// True track in decimal degrees clockwise from north. Can be null
        /// </summary>
        float? TrueTrack { get; }

        /// <summary>
        /// Whether or no the received position is a surface position
        /// </summary>
        bool OnGround { get; }
    }

    [JsonConverter(typeof(TrackPathConverter))]
    public class OpenSkyTrackPath : IOpenSkyTrackPath
    {
        public DateTime Time { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public float? BaroAltitude { get; set; }
        public float? TrueTrack { get; set; }
        public bool OnGround { get; set; }
    }
}
