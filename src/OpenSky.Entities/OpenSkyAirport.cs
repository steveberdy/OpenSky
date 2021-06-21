using Newtonsoft.Json;
using OpenSky.Converters;

namespace OpenSky.Entities
{
    /// <summary>
    /// Data for an airport's registration
    /// </summary>
    [JsonConverter(typeof(InterfaceConverter<IOpenSkyAirport, OpenSkyAirport>))]
    public interface IOpenSkyAirport
    {
        /// <summary>
        /// 4-character ICAO address for the airport
        /// </summary>
        [JsonProperty("icao")]
        string Icao { get; }

        /// <summary>
        /// 3-character IATA code for the airport
        /// </summary>
        [JsonProperty("iata")]
        string Iata { get; }

        /// <summary>
        /// Name of the airport
        /// </summary>
        [JsonProperty("name")]
        string Name { get; }

        /// <summary>
        /// The airport's city
        /// </summary>
        [JsonProperty("city")]
        string City { get; }

        /// <summary>
        /// Airport type
        /// </summary>
        [JsonProperty("type")]
        string Type { get; }

        /// <summary>
        /// Position information for the airport
        /// </summary>
        [JsonProperty("position")]
        IOpenSkyPosition Position { get; }

        /// <summary>
        /// The airport's continent
        /// </summary>
        [JsonProperty("continent")]
        string Continent { get; }

        /// <summary>
        /// The airport's country
        /// </summary>
        [JsonProperty("country")]
        string Country { get; }

        /// <summary>
        /// The airport's region
        /// </summary>
        [JsonProperty("region")]
        string Region { get; }

        /// <summary>
        /// The airport's municipality
        /// </summary>
        [JsonProperty("municipality")]
        string Municipality { get; }

        /// <summary>
        /// The airport's GPS code
        /// </summary>
        [JsonProperty("gpsCode")]
        string GPSCode { get; }

        /// <summary>
        /// URL for the airport's homepage
        /// </summary>
        [JsonProperty("homepage")]
        string Homepage { get; }

        /// <summary>
        /// URL for the airport's Wikipedia page
        /// </summary>
        [JsonProperty("wikipedia")]
        string Wikipedia { get; }
    }

    [JsonConverter(typeof(InterfaceConverter<IOpenSkyPosition, OpenSkyPosition>))]
    public interface IOpenSkyPosition
    {
        /// <summary>
        /// Longitude of the airport
        /// </summary>
        [JsonProperty("longitude")]
        float Longitude { get; }

        /// <summary>
        /// Latitude of the airport
        /// </summary>
        [JsonProperty("latitude")]
        float Latitude { get; }

        /// <summary>
        /// Altitude of the airport
        /// </summary>
        [JsonProperty("altitude")]
        float Altitude { get; }

        /// <summary>
        /// Whether or not the position of the airport is accurate
        /// </summary>
        [JsonProperty("reasonable")]
        bool Reasonable { get; }
    }

    public class OpenSkyAirport : IOpenSkyAirport
    {
        public string Icao { get; set; }
        public string Iata { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Type { get; set; }
        public IOpenSkyPosition Position { get; set; }
        public string Continent { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string Municipality { get; set; }
        public string GPSCode { get; set; }
        public string Homepage { get; set; }
        public string Wikipedia { get; set; }
    }

    public class OpenSkyPosition : IOpenSkyPosition
    {
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float Altitude { get; set; }
        public bool Reasonable { get; set; }
    }
}