using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dynu.API
{
    public class Domain : ClientBase
    {
        private const string PREFIX = "domain";
        public async Task<Model.Domain.Domain> DomainGetAsync(string domain)
        {
            return await Get<Model.Domain.Domain>($"{PREFIX}/get/{domain}", AuthenticatedClient);
        }
        public async Task<Model.Domain.Domain[]> DomainGetAsync()
        {
            return await Get<Model.Domain.Domain[]>($"{PREFIX}/domains", AuthenticatedClient);
        }
        public async Task<Model.Domain.Domain> DomainCancelAsync(string domain)
        {
            return await Delete<Model.Domain.Domain>($"{PREFIX}/cancel/{domain}", AuthenticatedClient);
        }
        public async Task<Model.Domain.Domain> DomainAutoRenewalEnableAsync(string domain)
        {
            return await Get<Model.Domain.Domain>($"{PREFIX}/domain/autorenewal_enable/{domain}", AuthenticatedClient);
        }
        public async Task<Model.Domain.Domain> DomainAutoRenewalDisableAsync(string domain)
        {
            return await Get<Model.Domain.Domain>($"{PREFIX}/domain/autorenewal_disable/{domain}", AuthenticatedClient);
        }
        public async Task<Model.Domain.Domain> DomainLockAsync(string domain)
        {
            return await Get<Model.Domain.Domain>($"{PREFIX}/domain/lock/{domain}", AuthenticatedClient);
        }
        public async Task<Model.Domain.Domain> DomainLockAsync(int domainId)
        {
            return await Get<Model.Domain.Domain>($"{PREFIX}/domain/lock/{domainId}", AuthenticatedClient);
        }
        public async Task<Model.Domain.Domain> DomainUnLockAsync(string domain)
        {
            return await Get<Model.Domain.Domain>($"{PREFIX}/domain/unlock/{domain}", AuthenticatedClient);
        }
        public async Task<Model.Domain.Domain> DomainUnLockAsync(int domainId)
        {
            return await Get<Model.Domain.Domain>($"{PREFIX}/domain/unlock/{domainId}", AuthenticatedClient);
        }

        public async Task<Model.Domain.NameServers> NameServersGetAsync(string domain)
        {
            return await Get<Model.Domain.NameServers>($"{PREFIX}/domain/name_servers/{domain}", AuthenticatedClient);
        }
        public async Task<Model.Domain.NameServers> NameServersGetAsync(int domainId)
        {
            return await Get<Model.Domain.NameServers>($"{PREFIX}/domain/name_servers/{domainId}", AuthenticatedClient);
        }
        public async Task<Model.Domain.NameServers> NameServerAddAsync(string domain, string[] names)
        {
            return await Post<Model.Domain.NameServers>($"{PREFIX}/domain/name_server_add/{domain}", "{\"name_servers\": [" + FixNames(names) + "]}", "application/json", AuthenticatedClient);
        }
        public async Task<Model.Domain.NameServers> NameServerAddAsync(int domainId, string[] names)
        {
            return await Post<Model.Domain.NameServers>($"{PREFIX}/domain/name_server_add/{domainId}", "{\"name_servers\": [" + FixNames(names) + "]}", "application/json", AuthenticatedClient);
        }
        public async Task<Model.Domain.NameServers> NameServerRemoveAsync(string domain, string[] names)
        {
            return await Post<Model.Domain.NameServers>($"{PREFIX}/domain/name_server_remove/{domain}", "{\"name_servers\": [" + FixNames(names) + "]}", "application/json", AuthenticatedClient);
        }
        public async Task<Model.Domain.NameServers> NameServerRemoveAsync(int domainId, string[] names)
        {
            return await Post<Model.Domain.NameServers>($"{PREFIX}/domain/name_server_remove/{domainId}", "{\"name_servers\": [" + FixNames(names) + "]}", "application/json", AuthenticatedClient);
        }
        public async Task<Model.Domain.NameServers> NameServerMakePrimaryAsync(string domain, string name)
        {
            return await Post<Model.Domain.NameServers>($"{PREFIX}/domain/name_server_primary/{domain}", "{\"name_servers\": \"" + name + "\"}", "application /json", AuthenticatedClient);
        }
        public async Task<Model.Domain.NameServers> NameServerMakePrimaryAsync(int domainId, string name)
        {
            return await Post<Model.Domain.NameServers>($"{PREFIX}/domain/name_server_primary/{domainId}", "{\"name_servers\": \"" + name + "\"}", "application /json", AuthenticatedClient);
        }

        private string FixNames(string[] names)
        {
            List<string> fixedNames = new List<string>();
            foreach (var n in names)
            {
                fixedNames.Add("\"" + n.Remove('\"') + "\"");
            }

            return string.Join(",", fixedNames);
        }
    }
}
