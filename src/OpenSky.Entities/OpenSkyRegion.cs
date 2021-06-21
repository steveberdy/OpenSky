namespace OpenSky.Entities
{
    /// <summary>
    /// Class representing a box region of latitude and longitude coordinates
    /// </summary>
    public class OpenSkyRegion
    {
        public OpenSkyRegion()
        {
        }

        public OpenSkyRegion(float lamin, float lamax, float lomin, float lomax)
        {
            MinLatitude = lamin;
            MaxLatitude = lamax;
            MinLongitude = lomin;
            MaxLongitude = lomax;
        }

        /// <summary>
        /// Minimum latitude for the region
        ///</summary>
        public float MinLatitude { get; set; }

        /// <summary>
        /// Maximum latitude for the region
        /// </summary>
        public float MaxLatitude { get; set; }

        /// <summary>
        /// Minimum longitude for the region
        /// </summary>
        public float MinLongitude { get; set; }

        /// <summary>
        /// Maximum longitude for the region
        /// </summary>
        public float MaxLongitude { get; set; }
    }
}