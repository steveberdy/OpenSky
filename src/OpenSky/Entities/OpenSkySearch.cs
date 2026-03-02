namespace OpenSky
{
    /// <summary>
    /// A search result
    /// </summary>
    public class OpenSkySearch
    {
        public OpenSkySearchResult[] Content { get; set; }
        public bool Last { get; set; }
        public int TotalElements { get; set; }
        // Other properties in the json, but not needed
    }

    public class OpenSkySearchResult
    {
        public string Icao24 { get; set; }
        public string Callsign { get; set; }
        public string Model { get; set; }
        public string Operator { get; set; }
        public string Country { get; set; }
    }
}