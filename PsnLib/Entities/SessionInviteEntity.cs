using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PsnLib.Entities
{
    public class SessionInviteEntity
    {
        public class FromUser
        {
            public string OnlineId { get; set; }
        }

        public class NpTitleDetail
        {
            public string NpTitleId { get; set; }
            public string NpCommunicationId { get; set; }
            public string NpTitleName { get; set; }
            public string NpTitleIconUrl { get; set; }
        }

        public class Invitation
        {
            public string InvitationId { get; set; }

            public string Message { get; set; }
            public bool SeenFlag { get; set; }
            public bool UsedFlag { get; set; }
            public string SessionId { get; set; }
            public DateTime ReceivedDate { get; set; }
            public DateTime UpdateDate { get; set; }
            public bool Expired { get; set; }
            public FromUser FromUser { get; set; }
            public List<string> AvailablePlatforms { get; set; }
            public string Subject { get; set; }
            public NpTitleDetail NpTitleDetail { get; set; }
        }

        public class Member
        {
            public string OnlineId { get; set; }
            public string Platform { get; set; }
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

        public int Start { get; set; }
        public int Size { get; set; }
        public int TotalResults { get; set; }
        public List<Invitation> Invitations { get; set; }
    }
}
