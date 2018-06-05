
using System;
using System.Net;
using System.Net.Http;

namespace Dynu.API.Model.IPUpdater
{
    public class IPUpdaterOptionsBuilder
    {
        public PortOption Port { get; private set; }
        public IAuth Authentication { get; private set; }
        public string Protocol { get; private set; }
        public string UserAgent { get; private set; }
        public HttpClientHandler Handler { get; private set; }

        public IPUpdaterOptionsBuilder()
        {
            WithAuthentication(new BasicHeaderAuth("", ""));
            WithPort(PortOption.Port443);
            WithUserAgent(".NET Dynu.API");
        }

        public IPUpdaterOptionsBuilder WithPort(PortOption port)
        {
            Port = port;
            switch (Port)
            {
                case PortOption.Port443:
                    Protocol = "https";
                    break;
                case PortOption.Port80:
                case PortOption.Port8245:
                    Protocol = "http";
                    break;
            }
            return this;
        }
        public IPUpdaterOptionsBuilder WithAuthentication(IAuth authentication)
        {
            Authentication = authentication;
            return this;
        }
        public IPUpdaterOptionsBuilder WithUserAgent(string userAgent)
        {
            UserAgent = userAgent;
            return this;
        }

        public IPUpdaterOptionsBuilder WithProxy(string host, int port, string username, string password)
        {
            Handler = new HttpClientHandler()
            {
                Proxy = new WebProxy()
                {
                    Address = new Uri($"{host}:{port}"),
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(username, password)
                },
                PreAuthenticate = true,
                UseDefaultCredentials = false
            };
            return this;
        }
        public IPUpdaterOptionsBuilder WithProxy(string host, int port)
        {
            Handler = new HttpClientHandler()
            {
                Proxy = new WebProxy()
                {
                    Address = new Uri($"{host}:{port}"),
                    UseDefaultCredentials = false
                }
            };
            return this;
        }
        public IPUpdaterOptionsBuilder WithProxy(IWebProxy proxy, bool preAuthenticate, bool useDefaultCredentials)
        {
            Handler = new HttpClientHandler()
            {
                Proxy = proxy,
                PreAuthenticate = preAuthenticate,
                UseDefaultCredentials = useDefaultCredentials
            };
            return this;
        }
    }
}
