using System;

namespace OpenSky
{
    public class OpenSkyException : Exception
    {
        public OpenSkyException()
        {
        }

        public OpenSkyException(string message) : base(message)
        {
        }
    }
}