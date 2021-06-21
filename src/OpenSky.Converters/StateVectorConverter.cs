using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using OpenSky.Entities;

namespace OpenSky.Converters
{
    public class StateVectorConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(OpenSkyStateVector);

        public override bool CanRead => true;

        public override bool CanWrite => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonArray = JArray.Load(reader);

            return new OpenSkyStateVector
            {
                Icao24 = jsonArray[0].Value<string>(),
                Callsign = jsonArray[1].Value<string>()?.Trim(),
                OriginCountry = jsonArray[2].Value<string>(),
                TimePosition = jsonArray[3].Value<int?>().FromUnixTimestamp(),
                LastContact = jsonArray[4].Value<int>(),
                Longitude = jsonArray[5].Value<float?>(),
                Latitude = jsonArray[6].Value<float?>(),
                BaroAltitude = jsonArray[7].Value<float?>(),
                OnGround = jsonArray[8].Value<bool>(),
                Velocity = jsonArray[9].Value<float?>(),
                TrueTrack = jsonArray[10].Value<float?>(),
                VerticalRate = jsonArray[11].Value<float?>(),
                Sensors = jsonArray[12].Value<int[]>(),
                GeoAltitude = jsonArray[13].Value<float?>(),
                Squawk = jsonArray[14].Value<string>(),
                Spi = jsonArray[15].Value<bool>(),
                PositionSource = jsonArray[16].Value<int>()
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => throw new NotImplementedException();
    }
}
