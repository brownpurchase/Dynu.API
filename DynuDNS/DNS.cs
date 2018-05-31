using System.Threading.Tasks;

namespace Dynu.API
{
    public class DNS : ClientBase
    {
        private const string PREFIX = "dns";

        public async Task<Model.DNS.Domain> DomainGetAsync(string domain)
        {
            return await Get<Model.DNS.Domain>($"{PREFIX}/get/{domain}", AuthenticatedClient);
        }
        public async Task<Model.DNS.Domain[]> DomainGetAsync()
        {
            return await Get<Model.DNS.Domain[]>($"{PREFIX}/domains", AuthenticatedClient);
        }
        public async Task<Model.DNS.Domain> DomainDeleteAsync(string domain)
        {
            return await Delete<Model.DNS.Domain>($"{PREFIX}/delete/{domain}", AuthenticatedClient);
        }
        public async Task<Model.DNS.Domain> DomainAddAsync(string domain, string ipv4Address, string ipv6Address)
        {
            return await Post<Model.DNS.Domain>($"{PREFIX}/add/{domain}", "{\"name\": \"" + domain + "\", \"ipv4_address\":\"" + ipv4Address + "\", \"ipv6_address\":\"" + ipv6Address + "\"}", "application/json", AuthenticatedClient);
        }


        public async Task<Model.DNS.Domain> IPv6EnableAsync(string domain)
        {
            return await Get<Model.DNS.Domain>($"{PREFIX}/enableipv6/{domain}", AuthenticatedClient);
        }
        public async Task<Model.DNS.Domain> IPv6EnableAsync(int domainId)
        {
            return await Get<Model.DNS.Domain>($"{PREFIX}/enableipv6/{domainId}", AuthenticatedClient);
        }
        public async Task<Model.DNS.Domain> IPv6DisableAsync(string domain)
        {
            return await Get<Model.DNS.Domain>($"{PREFIX}/disableipv6/{domain}", AuthenticatedClient);
        }
        public async Task<Model.DNS.Domain> IPv6DisableAsync(int domainId)
        {
            return await Get<Model.DNS.Domain>($"{PREFIX}/disableipv6/{domainId}", AuthenticatedClient);
        }
        public async Task<Model.DNS.Domain> IPv4WildcardAliasEnableAsync(string domain)
        {
            return await Get<Model.DNS.Domain>($"{PREFIX}/enableipv4wildcard/{domain}", AuthenticatedClient);
        }
        public async Task<Model.DNS.Domain> IPv4WildcardAliasDisableAsync(string domain)
        {
            return await Get<Model.DNS.Domain>($"{PREFIX}/disableipv4wildcard/{domain}", AuthenticatedClient);
        }
        public async Task<Model.DNS.Domain> IPv6WildcardAliasEnableAsync(string domain)
        {
            return await Get<Model.DNS.Domain>($"{PREFIX}/enableipv6wildcard/{domain}", AuthenticatedClient);
        }
        public async Task<Model.DNS.Domain> IPv6WildcardAliasDisableAsync(string domain)
        {
            return await Get<Model.DNS.Domain>($"{PREFIX}/disableipv6wildcard/{domain}", AuthenticatedClient);
        }

        public async Task<Model.DNS.Record[]> RecordsGetAsync(string domain)
        {
            return await Get<Model.DNS.Record[]>($"{PREFIX}/records/{domain}", AuthenticatedClient);
        }
        public async Task<Model.DNS.Record> RecordCreateAsync(Model.DNS.Record record)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(record);
            return await Post<Model.DNS.Record>($"{PREFIX}/record/add", json, "application/json", AuthenticatedClient);
        }
        public async Task<Model.DNS.Record> RecordGetAsync(int id)
        {
            return await Get<Model.DNS.Record>($"{PREFIX}/record/get/{id}", AuthenticatedClient);
        }
        public async Task<Model.DNS.Record> RecordEnableAsync(int id)
        {
            return await Get<Model.DNS.Record>($"{PREFIX}/record/enable/{id}", AuthenticatedClient);
        }
        public async Task<Model.DNS.Record> RecordDisableAsync(int id)
        {
            return await Get<Model.DNS.Record>($"{PREFIX}/record/disable/{id}", AuthenticatedClient);
        }
        public async Task<Model.DNS.Record> RecordDeleteAsync(int id)
        {
            return await Get<Model.DNS.Record>($"{PREFIX}/record/delete/{id}", AuthenticatedClient);
        }

    }
}
