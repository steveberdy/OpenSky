using System;

namespace OpenSky
{
    /// <summary>
    /// Response for aircraft states
    /// </summary>
    public class OpenSkyStates
    {
        public DateTime Time { get; set; }
        public OpenSkyStateVector[] States { get; set; }
    }
}
