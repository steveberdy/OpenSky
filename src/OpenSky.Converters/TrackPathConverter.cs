using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace OpenSky.Converters
{
    public class TrackPathConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(OpenSkyTrackPath);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonArray = JArray.Load(reader);

            return new OpenSkyTrackPath
            {
                Time = jsonArray[0].Value<int>().FromUnixTimestamp(),
                Latitude = jsonArray[1].Value<float?>(),
                Longitude = jsonArray[2].Value<float?>(),
                BaroAltitude = jsonArray[3].Value<float?>(),
                TrueTrack = jsonArray[4].Value<float?>(),
                OnGround = jsonArray[5].Value<bool>()
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => throw new NotImplementedException();
    }
}
