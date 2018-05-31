using JsonSubTypes;
using Newtonsoft.Json;

namespace Dynu.API.Model.DNS
{
    [JsonConverter(typeof(JsonSubtypes), "record_type")]
    public abstract class Record
    {
        public virtual string record_type { get; }
        public int id { get; set; }
        public int domain_id { get; set; }
        public string domain_name { get; set; }
        public string node_name { get; set; }
        public string hostname { get; set; }
        public int ttl { get; set; }
        public string content { get; set; }
        public bool state { get; set; }
    }
    public class WCA : Record
    {
        public override string record_type { get; } = "WCA";
        public string ipv4_address { get; set; }
    }
    public class NS : Record
    {
        public override string record_type { get; } = "NS";
        public string name_server { get; set; }
    }
    public class MX : Record
    {
        public override string record_type { get; } = "MX";
        public string host { get; set; }
        public int priority { get; set; }
    }
    public class Key : Record
    {
        public override string record_type { get; } = "Key";
        public string flags { get; set; }
        public string protocol { get; set; }
        public string algorithm { get; set; }
        public string key { get; set; }
    }
    public class HINFO : Record
    {
        public override string record_type { get; } = "HINFO";
        public string hardware { get; set; }
        public string operating_system { get; set; }
    }
    public class AFSDB : Record
    {
        public override string record_type { get; } = "AFSDB ";
        public int sub_type { get; set; }
    }
    public class A : Record
    {
        public override string record_type { get; } = "A ";
        public string location { get; set; }
        public string ipv4_address { get; set; }
    }
    public class AAAA : Record
    {
        public override string record_type { get; } = "AAAA ";
        public string location { get; set; }
        public string ipv6_address { get; set; }
    }
    public class PF : Record
    {
        public override string record_type { get; } = "PF ";
        public string host { get; set; }
        public int port { get; set; }
        public string title { get; set; }
        public string meta_keywords { get; set; }
        public string meta_description { get; set; }
        public bool cloak { get; set; }
        public bool include_query_string { get; set; }
        public bool use_dynamic_ipv4_address { get; set; }
        public bool use_dynamic_ipv6_address { get; set; }
    }
    public class PTR : Record
    {
        public override string record_type { get; } = "PTR ";
        public string host { get; set; }
    }
    public class RP : Record
    {
        public override string record_type { get; } = "RP ";
        public string mailbox { get; set; }
        public string txt_domain_name { get; set; }
    }
    public class SPF : Record
    {
        public override string record_type { get; } = "SPF ";
        public string text_data { get; set; }
    }
    public class TXT : Record
    {
        public override string record_type { get; } = "TXT ";
        public string text_data { get; set; }
    }
    public class SRV : Record
    {
        public override string record_type { get; } = "SRV ";
        public int port { get; set; }
        public int priority { get; set; }
        public int weight { get; set; }
        public string target { get; set; }
        public string service { get; set; }
        public string protocol { get; set; }
    }
    public class UF : Record
    {
        public override string record_type { get; } = "UF ";
        public string redirect_url { get; set; }
        public string title { get; set; }
        public string meta_keywords { get; set; }
        public string meta_description { get; set; }
        public bool cloak { get; set; }
        public bool include_query_string { get; set; }
    }
}
