using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PsnLib.Entities;
using PsnLib.Interfaces;
using PsnLib.Tools;

namespace PsnLib.Managers
{
    public class UserManager
    {
        private readonly IWebManager _webManager;

        public UserManager(IWebManager webManager)
        {
            _webManager = webManager;
        }

        public UserManager()
            : this(new WebManager())
        {
        }


        public async Task<UserEntity> GetUser(string userName, UserAccountEntity userAccountEntity)
        {
            try
            {
                var user = userAccountEntity.GetUserEntity();
                string url = string.Format(EndPoints.User, user.Region, userName);
                url += "&r=" + Guid.NewGuid();
                var result = await _webManager.GetData(new Uri(url), userAccountEntity);
                var userEntity = JsonConvert.DeserializeObject<UserEntity>(result.ResultJson);
                return userEntity;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting user", ex);
            }
        }
    }
}
