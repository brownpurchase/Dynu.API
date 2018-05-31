using System;

namespace Dynu.API.Model.Domain
{
    public class Domain
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string name { get; set; }
        public string unicode_name { get; set; }
        public string token { get; set; }
        public string state { get; set; }
        public string language { get; set; }
        public bool lockable { get; set; }
        public bool auto_renew { get; set; }
        public bool whois_protected { get; set; }
        public DateTime expires_on { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
