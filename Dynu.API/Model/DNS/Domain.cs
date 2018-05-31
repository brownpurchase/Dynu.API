using System;

namespace Dynu.API.Model.DNS
{
    public class Domain
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string name { get; set; }
        public string token { get; set; }
        public string state { get; set; }
        public string ipv4_address { get; set; }
        public string ipv6_address { get; set; }
        public bool wildcard_alias { get; set; }
        public int ttl { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
