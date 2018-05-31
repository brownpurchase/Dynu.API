using Dynu.API.Model.IPUpdater;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Dynu.API
{
    public class IPUpdater
    {
        private const string HOST = "api.dynu.com";
        private const string NIC_PATH = "/nic/update";
        private const string ETRN_PATH = "/etrn/update";

        private readonly IAuth _Auth;
        private readonly PortOption _PortOption;
        private readonly string PROTOCOL;

        public enum PortOption
        {
            Port443,
            Port80,
            Port8245
        }
        public IPUpdater(IAuth auth, PortOption portOption)
        {
            _Auth = auth;
            _PortOption = portOption;
            switch (_PortOption)
            {
                case PortOption.Port443:
                    PROTOCOL = "https";
                    break;
                case PortOption.Port80:
                case PortOption.Port8245:
                    PROTOCOL = "http";
                    break;
            }
        }
        public IPUpdater(IAuth auth) : this(auth, PortOption.Port443)
        {
        }

        /// <summary>
        /// This action is used to update the primary IP address for the domain name. Either the username or hostname can be supplied. 
        /// </summary>
        /// <returns><see cref="https://www.dynu.com/DynamicDNS/IP-Update-Protocol#responsecode"/></returns>
        /// <param name="criteria"></param>
        /// <param name="ipv4address">Optional. IPv4 address to be used for update. This field is OPTIONAL. If IP addrss is not sent as part of the request, the API server will use the IP address from which the request originates. The special IP address 10.0.0.0 is replaced with the IP address from which the request originates and you may use it with routers which disallow empty IP address field. </param>
        /// <param name="ipv6address">Optional. IPv6 address to be used for update. If IPv6 address is not sent as part of the request, the API server will NOT be able to update the IPv6 address and will only update IPv4 address for the hostname. </param>
        public async Task<string> UpdateDomain(UpdateDomainCriteria criteria, string ipv4address = null, string ipv6address = null)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria cannot be null.");

            string updated = (ipv4address == null ? "" : $"&myip={ipv4address}") + (ipv6address == null ? "" : $"&myipv6={ipv6address}");
            string url = $"{PROTOCOL}://{HOST}{NIC_PATH}?{criteria.QueryString}{updated}";
            return await MakeRequest(url);
        }

        /// <summary>
        /// This action is used to update the IP address of an alias/subdomain for the domain name. Please make sure to add this alias/subdomain in the 'Aliases/Subdomains' section in the control panel for the domain name. If 'Wildcard' option is enabled in the control panel, virtually all aliases/subdomains point to the primary IP address by default. 
        /// </summary>
        /// <returns><see cref="https://www.dynu.com/DynamicDNS/IP-Update-Protocol#responsecode"/></returns>
        /// <param name="hostname">Required. A single hostname whose alias/subdomain requires IP address update. </param>
        /// <param name="alias">Required. The alias/subdomain of hostname whose IP address requires update. </param>
        /// <param name="ipv4address">Optional. IP address to be used for update. This field is OPTIONAL. If IP address is not sent as part of the request, the API server will use the IP address from which the request originates. The special IP address 10.0.0.0 is replaced with the IP address from which the request originates and you may use it with routers which disallow empty IP address field. </param>
        /// <param name="ipv6address">Optional. IPv6 address to be used for update. If IPv6 address is not sent as part of the request, the API server will NOT be able to update the IPv6 address and will only update IPv4 address for the hostname. </param>
        public async Task<string> UpdateSubDomain(string hostname, string alias, string ipv4address = null, string ipv6address = null)
        {
            if (hostname == null)
                throw new ArgumentNullException("hostname cannot be null.");
            if (alias == null)
                throw new ArgumentNullException("alias cannot be null.");

            string updated = (ipv4address == null ? "" : $"&myip={ipv4address}") + (ipv6address == null ? "" : $"&myipv6={ipv6address}");
            string url = $"{PROTOCOL}://{HOST}{NIC_PATH}?hostname={hostname}&alias={alias}{updated}";
            return await MakeRequest(url);
        }
        /// <summary>
        /// This action is used when the server/router is going offline and would like to point the domain name to an offline message or offline URL. The offline action itself can be setup in the control panel . 
        /// </summary>
        /// <returns><see cref="https://www.dynu.com/DynamicDNS/IP-Update-Protocol#responsecode"/></returns>
        /// <param name="hostname"> Required. A single hostname whose IP address requires update. </param>
        /// <param name="offline"></param>
        public async Task<string> SetStatus(string hostname, bool offline)
        {
            if (hostname == null)
                throw new ArgumentNullException("hostname cannot be null.");

            string status = offline ? "yes" : "no";
            string url = $"{PROTOCOL}://{HOST}{NIC_PATH}?hostname={hostname}&offline={status}";
            return await MakeRequest(url);
        }
        /// <summary>
        /// This action is used to inform Dynu email servers to route emails for a given domain name to its primary email server. The mode of email service in the control panel should be setup as 'Email Store/Forward'. 
        /// </summary>
        /// <returns><see cref="https://www.dynu.com/DynamicDNS/IP-Update-Protocol#responsecode"/></returns>
        /// <param name="hostname">A single domain name whose emails are to be routed to its primary email server. </param>
        /// <param name="ipaddress">IP address of the primary email server. This field is OPTIONAL. If IP address is not sent as part of the request, the API server will use the IP address from which the request originates. The special IP address 10.0.0.0 is replaced with the IP address from which the request originates and you may use it with routers which disallow empty IP address field. </param>
        /// <param name="port"> The port number on which the primary email server is running. If your ISP is blocking inbound port 25, we recommend that you run your email server on a non-standard port such as 26 or 2525. </param>
        public async Task<string> EmailRouteUpdate(string hostname, string ipaddress, int port)
        {
            string url = $"{PROTOCOL}://{HOST}{ETRN_PATH}?hostname={hostname}&myip={ipaddress}&port={port}";
            return await MakeRequest(url);
        }
        private async Task<string> MakeRequest(string url)
        {
            using (var client = new WebClient())
            {
                _Auth.Apply(client, ref url);
                return await client.DownloadStringTaskAsync(url);
            }
        }

        public class UpdateDomainCriteria
        {
            public string QueryString { get; private set; }

            /// <summary>
            /// </summary>
            /// <param name="hostname">Optional. One or more comma-separated hostnames whose IP address requires update. If you wish to update the IP address of all hostnames in the account, use the 'username' parameter instead.</param>
            public UpdateDomainCriteria(string hostname)
            {
                QueryString = $"hostname={hostname}";
            }

            /// <summary>
            /// </summary>
            /// <param name="location">Optional. Use 'location' parameter if you want to update IP address for a collection of hostnames including those created using subdomains. Please note that the 'username' and 'password' parameters are mandatory when using location. The 'hostname' parameter is ignored when 'location' parameter is used. </param>
            /// <param name="username">Optional. Providing the username and no hostnames will update the IP address for all hostnames in the user account that have not been assigned a location name.</param>
            public UpdateDomainCriteria(string location = null, string username = null)
            {
                if (location == null && username == null)
                    throw new ArgumentNullException("Both location and username cannot be null");

                if (username != null)
                    QueryString = $"username={username}";

                if (location != null)
                    QueryString = QueryString == null ? $"location={location}" : $"&location={location}";
            }
        }
    }
}
