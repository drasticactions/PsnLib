using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsnLib.Entities.Auth
{
    public class CodeResult
    {
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
