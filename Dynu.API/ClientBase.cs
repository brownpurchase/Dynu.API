using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Dynu.API
{
    public abstract class ClientBase
    {
        private const string PROTOCOL = "https";
        private const string HOST = "api.dynu.com";
        private const string VERSION = "v1";
        private string _token;

        public async Task<Model.Authenticate> AuthenticateAsync(string clientId, string secret)
        {
            var client = CreateAuthenticationClient(clientId, secret);
            var ret = await Post<Model.Authenticate>("oauth2/token", "grant_type=client_credentials", "application/x-www-form-urlencoded", () => client).ConfigureAwait(false);

            if(!string.IsNullOrEmpty(ret.accessToken))
            {
                _token = ret.accessToken;
            }

            return ret;
        }
        public async Task<Model.Ping> PingAsync()
        {
            return await Post<Model.Ping>("ping", "", "application/json", AuthenticatedClient).ConfigureAwait(false);
        }

        protected async Task<T> Post<T>(string url, string data, string contentType, Func<HttpClient> factory)
        {
            using (var client = factory())
            {
                var msg = await client.PostAsync($"{PROTOCOL}://{HOST}/{VERSION}/{url}", new StringContent(data, Encoding.UTF8, contentType)).ConfigureAwait(false);
                if (msg.IsSuccessStatusCode)
                {
                    string json = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
                }
                else
                {
                    throw new HttpRequestException(msg.ReasonPhrase);
                }
            }
        }
        protected async Task<T> Get<T>(string url, Func<HttpClient> factory)
        {
            using (var client = factory())
            {
                var msg = await client.GetAsync($"{PROTOCOL}://{HOST}/{VERSION}/{url}").ConfigureAwait(false);
                if (msg.IsSuccessStatusCode)
                {
                    string json = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
                }
                else
                {
                    throw new HttpRequestException(msg.ReasonPhrase);
                }
            }
        }
        protected async Task<T> Delete<T>(string url, Func<HttpClient> factory)
        {
            using (var client = factory())
            {
                var msg = await client.DeleteAsync($"{PROTOCOL}://{HOST}/{VERSION}/{url}").ConfigureAwait(false);
                if (msg.IsSuccessStatusCode)
                {
                    string json = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
                }
                else
                {
                    throw new HttpRequestException(msg.ReasonPhrase);
                }
            }
        }
        protected HttpClient AuthenticatedClient()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue("Bearer", _token);

            return client;
        }
        private HttpClient CreateAuthenticationClient(string clientId, string secret)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders
                .Host = HOST;

            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders
                .AcceptLanguage
                .Add(new StringWithQualityHeaderValue("en_US"));
            client.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{secret}")));

            return client;
        }
    }
}
