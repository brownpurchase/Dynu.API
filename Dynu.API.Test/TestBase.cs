using System.Threading.Tasks;
using Xunit;

namespace Dynu.API.Test
{
    public abstract class TestBase
    {
        private string clientId = "c4f8efc9-7722-47a1-899a-fd9375a17048";
        private string secret = "7ShQ6JXD78M9ScpafWWgh1pW6vzRKz";

        protected async Task<T> PreAuth<T>() where T : ClientBase, new()
        {
            var t = new T();
            var result = await t.AuthenticateAsync(clientId, secret);

            Assert.NotNull(result);
            Assert.NotNull(result.accessToken);
            Assert.NotNull(result.tokenType);
            Assert.Equal("Bearer", result.tokenType);
            Assert.Equal(28800, result.expiresIn);

            return t;
        }
    }
}
