using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace OpenSky.Converters
{
    public class FloatUnixTimeConverter : DateTimeConverterBase
    {
        public override bool CanWrite => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                var parsed = (double)reader.Value;
                var second = (int)parsed;
                return second.FromUnixTimestamp();
            }
            catch
            {
                // Timestamps in milliseconds are too large for doubles
                var second = (long)reader.Value;
                return second.FromUnixTimestamp();
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => throw new NotImplementedException();
    }
}
