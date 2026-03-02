using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenSky.Converters
{
    public class TrackPathConverter : JsonConverter<OpenSkyTrackPath>
    {
        public override OpenSkyTrackPath Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new OpenSkyTrackPath
            {
                Time = reader.GetInt32().FromUnixTimestamp(),
                Latitude =reader.TryGetSingle(out float _lat) ? _lat : null,
                Longitude =  reader.TryGetSingle(out float _lng) ? _lng : null,
                BaroAltitude = reader.TryGetSingle(out float _ba) ? _ba : null,
                TrueTrack = reader.TryGetSingle(out float _tt) ? _tt : null,
                OnGround = reader.GetBoolean()
            };
        }

        public override void Write(Utf8JsonWriter writer, OpenSkyTrackPath value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
