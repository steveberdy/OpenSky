using Newtonsoft.Json;
using OpenSky.Converters;

namespace OpenSky
{
    /// <summary>
    /// A search result
    /// </summary>
    [JsonConverter(typeof(InterfaceConverter<IOpenSkySearch, OpenSkySearch>))]
    internal interface IOpenSkySearch
    {
        [JsonProperty("content")]
        OpenSkySearchResult[] Content { get; }

        [JsonProperty("last")]
        bool Last { get; }

        [JsonProperty("totalElements")]
        int TotalElements { get; }

        // Other properties in the json, but not needed
    }

    [JsonConverter(typeof(InterfaceConverter<IOpenSkySearchResult, OpenSkySearchResult>))]
    internal interface IOpenSkySearchResult
    {
        [JsonProperty("icao24")]
        string Icao24 { get; }

        [JsonProperty("registration")]
        string Callsign { get; }

        [JsonProperty("model")]
        string Model { get; }

        [JsonProperty("operator")]
        string Operator { get; }

        [JsonProperty("country")]
        string Country { get; }
    }

    public class OpenSkySearch : IOpenSkySearch
    {
        public OpenSkySearchResult[] Content { get; set; }
        public bool Last { get; set; }
        public int TotalElements { get; set; }
    }

    public class OpenSkySearchResult : IOpenSkySearchResult
    {
        public string Icao24 { get; set; }
        public string Callsign { get; set; }
        public string Model { get; set; }
        public string Operator { get; set; }
        public string Country { get; set; }
    }
}