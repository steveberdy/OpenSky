using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenSky.Converters
{
    public class StateVectorConverter : JsonConverter<OpenSkyStateVector>
    {
        public override OpenSkyStateVector Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new OpenSkyStateVector
            {
                Icao24 = reader.GetString(),
                Callsign = reader.GetString()?.Trim(),
                OriginCountry = reader.GetString(),
                TimePosition = reader.TryGetInt32(out int _tp) ? _tp.FromUnixTimestamp() : null,
                LastContact = reader.GetInt32(),
                Longitude = reader.TryGetSingle(out float _lng) ? _lng : null,
                Latitude = reader.TryGetSingle(out float _lat) ? _lat : null,
                BaroAltitude = reader.TryGetSingle(out float _ba) ? _ba : null,
                OnGround = reader.GetBoolean(),
                Velocity = reader.TryGetSingle(out float _vel) ? _vel : null,
                TrueTrack = reader.TryGetSingle(out float _tt) ? _tt : null,
                VerticalRate = reader.TryGetSingle(out float _vr) ? _vr : null,
                Sensors = JsonSerializer.Deserialize<int[]>(ref reader),
                GeoAltitude = reader.TryGetSingle(out float  _ga) ? _ga : null,
                Squawk = reader.GetString(),
                Spi = reader.GetBoolean(),
                PositionSource = reader.GetInt32()
            };
        }

        public override void Write(Utf8JsonWriter writer, OpenSkyStateVector value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
