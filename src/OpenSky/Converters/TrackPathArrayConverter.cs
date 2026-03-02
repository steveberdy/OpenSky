using Newtonsoft.Json;
using System;

namespace OpenSky.Converters
{
    public class TrackPathArrayConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(OpenSkyTrackPath[]);

        public override bool CanRead => true;

        public override bool CanWrite => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartArray)
            {
                return serializer.Deserialize<OpenSkyTrackPath[]>(reader);
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => throw new NotImplementedException();
    }
}
