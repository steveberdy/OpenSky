using System;

namespace OpenSky
{
    /// <summary>
    /// Track Path
    /// </summary>
    public class OpenSkyTrackPath
    {
        /// <summary>
        /// Time of the given waypoint
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// WGS-84 latitude in decimal degrees. Can be null
        /// </summary>
        public float? Latitude { get; set; }
        /// <summary>
        /// WGS-84 longitude in decimal degrees. Can be null
        /// </summary>
        public float? Longitude { get; set; }
        /// <summary>
        /// Barometric altitude in meters. Can be null
        /// </summary>
        public float? BaroAltitude { get; set; }
        /// <summary>
        /// True track in decimal degrees clockwise from north. Can be null
        /// </summary>
        public float? TrueTrack { get; set; }
        /// <summary>
        /// Whether or no the received position is a surface position
        /// </summary>
        public bool OnGround { get; set; }
    }
}
