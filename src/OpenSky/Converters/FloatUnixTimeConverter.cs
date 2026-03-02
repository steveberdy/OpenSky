using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenSky.Converters
{
    public class FloatUnixTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                var timestamp = (int)reader.GetDouble();
                return timestamp.FromUnixTimestamp();
            }
            catch
            {
                // Timestamps in milliseconds are too large for doubles
                var timestamp = reader.GetInt64();
                return timestamp.FromUnixTimestamp();
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
