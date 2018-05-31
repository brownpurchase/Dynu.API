using System;
using System.Threading.Tasks;
using Xunit;

namespace Dynu.API.Test
{
    public class DNSTest : TestBase
    {
        private void AssertDomain(Model.DNS.Domain domain)
        {
            Assert.NotNull(domain);
            Assert.NotNull(domain.name);
            Assert.NotNull(domain.token);
            Assert.NotNull(domain.ipv4_address);
            Assert.NotNull(domain.ipv6_address);
            Assert.True(domain.id > 0);
            Assert.True(domain.user_id > 0);
            Assert.Equal("Complete", domain.state);
        }
        private void AssertRecord(Model.DNS.Record record)
        {
        }
        [Theory]
        [InlineData("test.com")]
        public async Task GetDomain(string domain)
        {
            var result = await (await PreAuth<DNS>()).DomainGetAsync(domain);

            AssertDomain(result);

        }
        [Fact]
        public async Task GetDomains()
        {
            var result = await (await PreAuth<DNS>()).DomainGetAsync();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            foreach (var domain in result)
            {
                AssertDomain(domain);
            }
        }
        //[Fact]
        //public void DeleteDomain()
        //{
        //    var result = PreAuth<DNS>().DeleteDomainAsync("").Result;
        //}
        [Theory]
        [InlineData("test.com", "127.0.0.1", "")]
        public async Task AddDomain(string domain, string ipv4address, string ipv6address)
        {
            var result = await (await PreAuth<DNS>()).DomainAddAsync(domain, ipv4address, ipv6address);
            AssertDomain(result);
        }
        [Theory]
        [InlineData("test.com")]
        public async Task EnableIPv6(string domain)
        {
            var result = await (await PreAuth<DNS>()).IPv6EnableAsync(domain);
            AssertDomain(result);
        }
        [Theory]
        [InlineData("test.com")]
        public async Task DisableIPv6(string domain)
        {
            var result = await (await PreAuth<DNS>()).IPv6DisableAsync(domain);
            AssertDomain(result);
        }
        [Theory]
        [InlineData("test.com")]
        public async Task EnableIPv6Wildcard(string domain)
        {
            var result = await (await PreAuth<DNS>()).IPv6WildcardAliasEnableAsync(domain);
            AssertDomain(result);
        }
        [Theory]
        [InlineData("test.com")]
        public async Task DisableIPv6Wildcard(string domain)
        {
            var result = await (await PreAuth<DNS>()).IPv6WildcardAliasDisableAsync(domain);
            AssertDomain(result);
        }
        [Theory]
        [InlineData("test.com")]
        public async Task EnableIPv4Wildcard(string domain)
        {
            var result = await (await PreAuth<DNS>()).IPv4WildcardAliasEnableAsync(domain);
            AssertDomain(result);
        }
        [Theory]
        [InlineData("test.com")]
        public async Task DisableIPv4Wildcard(string domain)
        {
            var result = await (await PreAuth<DNS>()).IPv4WildcardAliasDisableAsync(domain);
            AssertDomain(result);
        }

        [Theory]
        [InlineData("test.com")]
        public async Task GetRecords(string domain)
        {
            var result = await (await PreAuth<DNS>()).RecordsGetAsync(domain);

            foreach (var record in result)
            {
                AssertRecord(record);
            }
        }
    }
}
