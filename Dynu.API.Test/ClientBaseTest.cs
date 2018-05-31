using System.Threading.Tasks;
using Xunit;

namespace Dynu.API.Test
{
    public class ClientBaseTest : TestBase
    {
        [Fact]
        public async Task Ping()
        {
            var ping = await(await PreAuth<DNS>()).PingAsync();

            Assert.Equal("Success", ping.type);
        }
    }
}
