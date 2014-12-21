﻿using System;
using Newtonsoft.Json.Linq;

namespace PsnLib.Entities
{
    public class UserAccountEntity
    {
        private User _entity;
        private Boolean _isCalled;
        private readonly AccountData _data;

        public UserAccountEntity(string accessToken, string refreshToken)
        {
            _data = new AccountData(accessToken, refreshToken, -1);
            _entity = null;
            _isCalled = false;
        }

        public String GetAccessToken()
        {
            if (GetUnixTime(DateTime.Now) < _data.RefreshTime)
                return _data.AccessToken;
            if (_isCalled) return _data.AccessToken;
            _isCalled = true;
            return "refresh";
        }

        public bool HasAccessToken()
        {
            return string.IsNullOrEmpty(_data.AccessToken);
        }

        public void SetUserEntity(User entity)
        {
            _entity = entity;
        }

        public User GetUserEntity()
        {
            return _entity;
        }

        public void SetAccessToken(String token, String refresh)
        {
            _data.AccessToken = token;
            _data.RefreshToken = refresh;
            if (_data.RefreshToken != null)
            {
                _isCalled = false;
            }
        }

        public void SetRefreshTime(long time)
        {
            _data.RefreshTime = GetUnixTime(DateTime.Now) + time;
            _data.StartTime = GetUnixTime(DateTime.Now);
        }

        public string GetRefreshToken()
        {
            return _data.RefreshToken;
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

        private class AccountData
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