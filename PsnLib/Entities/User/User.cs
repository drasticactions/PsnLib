using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsnLib.Entities.User
{
    public class User
    {
        [JsonProperty("onlineId")]
        public string OnlineId { get; set; }

        [JsonProperty("avatarUrls")]
        public AvatarUrl[] AvatarUrls { get; set; }

        [JsonProperty("aboutMe")]
        public string AboutMe { get; set; }

        [JsonProperty("languagesUsed")]
        public string[] LanguagesUsed { get; set; }

        [JsonProperty("plus")]
        public int Plus { get; set; }

        [JsonProperty("trophySummary")]
        public TrophySummary TrophySummary { get; set; }

        [JsonProperty("relation")]
        public string Relation { get; set; }

        [JsonProperty("presence")]
        public Presence Presence { get; set; }

        [JsonProperty("personalDetail")]
        public PersonalDetail PersonalDetail { get; set; }

        [JsonProperty("usePersonalDetailInGame")]
        public bool UsePersonalDetailInGame { get; set; }

        [JsonProperty("isOfficiallyVerified")]
        public bool IsOfficiallyVerified { get; set; }
    }

    public class Presence
    {

        [JsonProperty("primaryInfo")]
        public PrimaryInfo PrimaryInfo { get; set; }
    }

    public class PrimaryInfo
    {

        [JsonProperty("onlineStatus")]
        public string OnlineStatus { get; set; }

        [JsonProperty("lastOnlineDate")]
        public string LastOnlineDate { get; set; }
    }

    public class TrophySummary
    {

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("progress")]
        public int Progress { get; set; }

        [JsonProperty("earnedTrophies")]
        public EarnedTrophies EarnedTrophies { get; set; }

        public int TotalTrophies { get; set; }
    }

    public class AvatarUrl
    {

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("avatarUrl")]
        public string AvatarUrlLink { get; set; }
    }

    public class EarnedTrophies
    {

        [JsonProperty("platinum")]
        public int Platinum { get; set; }

        [JsonProperty("gold")]
        public int Gold { get; set; }

        [JsonProperty("silver")]
        public int Silver { get; set; }

        [JsonProperty("bronze")]
        public int Bronze { get; set; }
    }

    public class PersonalDetail
    {

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }
    }
}
