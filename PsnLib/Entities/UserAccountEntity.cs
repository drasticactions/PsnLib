using System;
using Newtonsoft.Json.Linq;

namespace PsnLib.Entities
{
    public class UserAccountEntity
    {
        public User Entity;
        private Boolean _isCalled;
        public readonly AccountData Data;

        public UserAccountEntity(string accessToken, string refreshToken)
        {
            Data = new AccountData(accessToken, refreshToken, -1);
            Entity = null;
            _isCalled = false;
        }

        public String GetAccessToken()
        {
            if (GetUnixTime(DateTime.Now) < Data.RefreshTime)
                return Data.AccessToken;
            if (_isCalled) return Data.AccessToken;
            _isCalled = true;
            return "refresh";
        }

        public bool HasAccessToken()
        {
            return !string.IsNullOrEmpty(Data.AccessToken);
        }

        public void SetUserEntity(User entity)
        {
            Entity = entity;
        }

        public User GetUserEntity()
        {
            return Entity;
        }

        public void SetAccessToken(String token, String refresh)
        {
            Data.AccessToken = token;
            Data.RefreshToken = refresh;
            if (Data.RefreshToken != null)
            {
                _isCalled = false;
            }
        }

        public void SetRefreshTime(long time)
        {
            Data.RefreshTime = GetUnixTime(DateTime.Now) + time;
            Data.StartTime = GetUnixTime(DateTime.Now);
        }

        public string GetRefreshToken()
        {
            return Data.RefreshToken;
        }

        public static long GetUnixTime(DateTime time)
        {
            time = time.ToUniversalTime();
            var timeSpam = time - (new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local));
            return (long) timeSpam.TotalSeconds;
        }

        public override string ToString()
        {
            return GetAccessToken() + ":" + GetRefreshToken();
        }

        public static User ParseUser(string json)
        {
            try
            {
                var o = JObject.Parse(json);
                return new User
                {
                    AccountId = (string) o["accountId"] ?? string.Empty,
                    MAccountId = (string) o["mAccountId"] ?? string.Empty,
                    Region = (string) o["region"] ?? string.Empty,
                    Language = (string) o["language"] ?? string.Empty,
                    OnlineId = (string) o["onlineId"] ?? string.Empty,
                    Age = (string) o["age"] ?? string.Empty,
                    DateOfBirth = (string) o["dateOfBirth"] ?? string.Empty,
                    CommunityDomain = (string) o["communityDomain"] ?? string.Empty,
                    Ps4Available = o["ps4Available"] != null && (bool) o["ps4Available"],
                    SubAccount = o["subaccount"] != null && (bool) o["subaccount"]
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to parse user", ex);
            }
        }

        public class AccountData
        {
            public string AccessToken;
            public long RefreshTime;
            public string RefreshToken;
            public long StartTime;

            public AccountData(String token, String refresh, int time)
            {
                AccessToken = token;
                RefreshToken = refresh;
                RefreshTime = GetUnixTime(DateTime.Now) + time;
                StartTime = GetUnixTime(DateTime.Now);
            }
        }

        public class User
        {
            public string AccountId { get; set; }
            public string MAccountId { get; set; }
            public string Region { get; set; }
            public string Language { get; set; }
            public string OnlineId { get; set; }
            public string Age { get; set; }
            public string DateOfBirth { get; set; }
            public string CommunityDomain { get; set; }
            public bool SubAccount { get; set; }
            public bool Ps4Available { get; set; }
        }
    }
}