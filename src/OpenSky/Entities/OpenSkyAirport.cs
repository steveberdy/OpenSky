namespace OpenSky
{
    /// <summary>
    /// Data for an airport's registration
    /// </summary>
    public class OpenSkyAirport
    {
        /// <summary>
        /// 4-character ICAO address for the airport
        /// </summary>
        public string Icao { get; set; }
        /// <summary>
        /// 3-character IATA code for the airport
        /// </summary>
        public string Iata { get; set; }
        /// <summary>
        /// Name of the airport
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The airport's city
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Airport type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Position information for the airport
        /// </summary>
        public OpenSkyPosition Position { get; set; }
        /// <summary>
        /// The airport's continent
        /// </summary>
        public string Continent { get; set; }
        /// <summary>
        /// The airport's country
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// The airport's region
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// The airport's municipality
        /// </summary>
        public string Municipality { get; set; }
        /// <summary>
        /// The airport's GPS code
        /// </summary>
        public string GPSCode { get; set; }
        /// <summary>
        /// URL for the airport's homepage
        /// </summary>
        public string Homepage { get; set; }
        /// <summary>
        /// URL for the airport's Wikipedia page
        /// </summary>
        public string Wikipedia { get; set; }
    }

    public class OpenSkyPosition
    {
        /// <summary>
        /// Longitude of the airport
        /// </summary>
        public float Longitude { get; set; }
        /// <summary>
        /// Latitude of the airport
        /// </summary>
        public float Latitude { get; set; }
        /// <summary>
        /// Altitude of the airport
        /// </summary>
        public float Altitude { get; set; }
        /// <summary>
        /// Whether or not the position of the airport is accurate
        /// </summary>
        public bool Reasonable { get; set; }
    }
}