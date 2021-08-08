using Xunit;

namespace OpenSky.Tests
{
    public class OpenSkyClientTests
    {
        private readonly OpenSkyClient client = new();

        [Fact]
        public async void Test_GetStates()
        {
            var res = await client.GetStates();
            Assert.NotNull(res);
            Assert.True(res.States.Length > 0);
        }
    }
}