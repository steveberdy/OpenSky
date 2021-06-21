using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OpenSky.Converters;

namespace OpenSky.Entities
{
    /// <summary>
    /// Response for aircraft states
    /// </summary>
    [JsonConverter(typeof(InterfaceConverter<IOpenSkyStates, OpenSkyStates>))]
    public interface IOpenSkyStates
    {
        /// <summary>
        /// The time which the state vectors in this response are associated with
        /// </summary>  
        [JsonConverter(typeof(UnixDateTimeConverter))]
        [JsonProperty("time")]
        DateTime Time { get; }

        /// <summary>
        /// The state vectors
        /// </summary>  
        [JsonConverter(typeof(StateVectorArrayConverter))]
        [JsonProperty("states")]
        IOpenSkyStateVector[] States { get; }
    }

    public class OpenSkyStates : IOpenSkyStates
    {
        public DateTime Time { get; set; }
        public IOpenSkyStateVector[] States { get; set; }
    }
}
