using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsnLib.Entities.Auth
{
    public class AuthCheck
    {
        [JsonProperty("npsso")]
        public string Npsso { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("service_entity")]
        public string ServiceEntity { get; set; }
    }
}
