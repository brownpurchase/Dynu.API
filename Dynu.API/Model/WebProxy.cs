using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;

namespace Dynu.API.Model
{
    public class WebProxy : IWebProxy
    {
        public WebProxy() : this((Uri)null, null) { }

        public WebProxy(Uri Address) : this(Address, null) { }

        public WebProxy(Uri Address, ICredentials Credentials)
        {
            this.Address = Address;
            this.Credentials = Credentials;
        }

        public WebProxy(string Host, int Port)
            : this(new Uri("http://" + Host + ":" + Port.ToString(CultureInfo.InvariantCulture)), null)
        {
        }

        public WebProxy(string Address)
            : this(CreateProxyUri(Address), null)
        {
        }

        public WebProxy(string Address, ICredentials Credentials)
            : this(CreateProxyUri(Address), Credentials)
        {
        }

        public Uri Address { get; set; }

        public ICredentials Credentials { get; set; }

        public bool UseDefaultCredentials
        {
            get { return Credentials == CredentialCache.DefaultCredentials; }
            set { Credentials = value ? CredentialCache.DefaultCredentials : null; }
        }

        public Uri GetProxy(Uri destination)
        {
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            return IsBypassed(destination) ? destination : Address;
        }

        private static Uri CreateProxyUri(string address) =>
            address == null ? null :
            address.IndexOf("://") == -1 ? new Uri("http://" + address) :
            new Uri(address);

        public bool IsBypassed(Uri host)
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            return Address == null || host.IsLoopback;
        }
    }
}
