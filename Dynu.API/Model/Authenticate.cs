namespace Dynu.API.Model
{
    public class Authenticate
    {
        public string scope { get; set; }
        public string accessToken { get; set; }
        public string tokenType { get; set; }
        public int expiresIn { get; set; }
        public string[] roles { get; set; }

        public bool CanAccessDomain { get { return scope.Contains("https://api.dynu.com/v1/Domain/.*"); } }
        public bool CanAccessDNS { get { return scope.Contains("https://api.dynu.com/v1/DNS/.* "); } }
        public bool CanAccessEmail { get { return scope.Contains("https://api.dynu.com/v1/Email/.*"); } }
    }
}
