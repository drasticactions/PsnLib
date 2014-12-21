using System;
using System.Collections.Generic;

namespace PsnLib.Entities
{
    public class SessionInviteDetailEntity
    {

        public DateTime ReceivedDate { get; set; }
        public bool SeenFlag { get; set; }
        public bool Expired { get; set; }
        public string Message { get; set; }
        public List<string> AvailablePlatforms { get; set; }
        public FromUser fromUser { get; set; }
        public bool UsedFlag { get; set; }
        public Session session { get; set; }
        public NpTitleDetail npTitleDetail { get; set; }

        public class FromUser
        {
            public string OnlineId { get; set; }
        }

        public class Member
        {
            public string OnlineId { get; set; }
            public string Platform { get; set; }

            public string AvatarUrl { get; set; }
        }

        public class Session
        {
            public string SessionId { get; set; }
            public string NpTitleType { get; set; }
            public string SessionType { get; set; }
            public string SessionPrivacy { get; set; }
            public int SessionMaxUser { get; set; }
            public string SessionName { get; set; }
            public string SessionStatus { get; set; }
            public long SessionCreateTimestamp { get; set; }
            public string SessionCreator { get; set; }
            public List<Member> Members { get; set; }
        }

        public class NpTitleDetail
        {
            public string NpTitleId { get; set; }
            public string NpCommunicationId { get; set; }
            public string NpTitleName { get; set; }
            public string NpTitleIconUrl { get; set; }
        }

    }
}
