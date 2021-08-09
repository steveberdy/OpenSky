using System;
using System.Threading.Tasks;
using System.Linq;
using Xunit;

namespace OpenSky.Tests
{
    public class OpenSkyClientTests
    {
        private readonly OpenSkyClient client = new();
        private OpenSkyStateVector[] allStates = default;

        private async Task GetAllStates()
        {
            if (allStates == null)
            {
                allStates = (await client.GetStates()).States;
            }
        }

        [Fact]
        public async void Test_GetStates()
        {
            await GetAllStates();

            Assert.NotNull(allStates);
            Assert.True(allStates.Length > 0);
        }

        [Fact]
        public async void Test_GetState()
        {
            await GetAllStates();

            Assert.NotNull(allStates);
            var icao24s = allStates.Select(x => x.Icao24).ToArray();
            var res = await client.GetState(icao24s[0]);
            Assert.NotNull(res);
            Assert.True(res.States.Length == 1);
        }

        [Fact]
        public async void Test_GetStates_Multiple()
        {
            await GetAllStates();

            var icao24s = allStates.Select(x => x.Icao24).ToArray();
            var res = await client.GetStates(icao24s.Where(x => x.StartsWith("71c")));
            Assert.NotNull(res);
            Assert.True(res.States.Length > 0);
        }

        [Fact]
        public async void Test_GetStates_Region()
        {
            var res = await client.GetStates(region: new OpenSkyRegion
            {
                MinLatitude = 45.8389f,
                MaxLatitude = 47.8229f,
                MinLongitude = 5.9962f,
                MaxLongitude = 10.5226f
            });
            Assert.NotNull(res);
        }

        [Fact]
        public async void Test_GetStates_Fail_TimeRange_UTC()
        {
            await GetAllStates();

            // The time range is not long enough
            var icao24s = allStates.Select(x => x.Icao24).Where(x => x.StartsWith("71c"));
            await Assert.ThrowsAsync<OpenSkyException>(async () =>
            {
                await client.GetStates(icao24s, DateTime.UtcNow.Subtract(new TimeSpan(2, 0, 0)));
            });
        }

        [Fact]
        public async void Test_GetStates_Fail_TimeRange_Local()
        {
            await GetAllStates();

            // An extra test that makes sure local times are subtracted properly
            var icao24s = allStates.Select(x => x.Icao24).Where(x => x.StartsWith("71c"));
            await Assert.ThrowsAsync<OpenSkyException>(async () =>
            {
                await client.GetStates(icao24s, DateTime.Now.Subtract(new TimeSpan(2, 0, 0)));
            });
        }

        [Fact]
        public async void Test_GetStates_Fail_Region()
        {
            // Invalid region box size
            await Assert.ThrowsAsync<OpenSkyException>(async () =>
            {
                await client.GetStates(region: new OpenSkyRegion
                {
                    MinLatitude = 1,
                    MaxLatitude = -1,
                    MinLongitude = 1,
                    MaxLongitude = -1
                });
            });
        }
    }
}