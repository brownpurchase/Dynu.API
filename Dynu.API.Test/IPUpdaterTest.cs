using Dynu.API.Model.IPUpdater;
using System.Threading.Tasks;
using Xunit;

namespace Dynu.API.Test
{
    public class IPUpdaterTest
    {
        [Theory]
        [InlineData("username", "password", "test.com", "subdomain")]
        public async Task UpdateIP(string username, string password, string domain, string subdomain)
        {
            IPUpdater updater = new IPUpdater(new BasicHeaderAuth(username,  password));
            await updater.UpdateSubDomain(domain, subdomain, null, null);
        }
    }
}
