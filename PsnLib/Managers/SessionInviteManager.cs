using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PsnLib.Entities;
using PsnLib.Interfaces;
using PsnLib.Tools;

namespace PsnLib.Managers
{
    public class SessionInviteManager
    {
        private readonly IWebManager _webManager;

        public SessionInviteManager(IWebManager webManager)
        {
            _webManager = webManager;
        }

        public SessionInviteManager()
            : this(new WebManager())
        {
        }

        public async Task<SessionInviteEntity> GetSessionInvites(int offset, UserAccountEntity userAccountEntity)
        {
            try
            {
                var user = userAccountEntity.GetUserEntity();
                string url = string.Format(EndPoints.SessionInformation, user.Region, user.OnlineId, user.Language, offset);
                var result = await _webManager.GetData(new Uri(url), userAccountEntity);
                var sessionInvite = JsonConvert.DeserializeObject<SessionInviteEntity>(result.ResultJson);
                return sessionInvite;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get session invites", ex);
            }
        }

        public async Task<SessionInviteDetailEntity> GetInviteInformation(string inviteId,
            UserAccountEntity userAccountEntity)
        {
            try
            {
                var user = userAccountEntity.GetUserEntity();
                string url = string.Format(EndPoints.InviteInformation, user.Region, user.OnlineId, inviteId, user.Language);
                var result = await _webManager.GetData(new Uri(url), userAccountEntity);
                var sessionInvite = JsonConvert.DeserializeObject<SessionInviteDetailEntity>(result.ResultJson);
                return sessionInvite;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get invite information", ex);
            }
        }
    }
}
