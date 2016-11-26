using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsnLib.Entities.Auth
{
    public class AuthResult
    {
        [JsonProperty("npsso")]
        public string NPSSO { get; set; }

        [JsonProperty("ticket_uuid")]
        public string TicketUuid { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        public bool IsTwoFactorCode
        {
           get
            {
                return !string.IsNullOrEmpty(TicketUuid);
            }
        }
    }
}
