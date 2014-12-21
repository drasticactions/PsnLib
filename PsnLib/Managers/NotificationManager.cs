using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PsnLib.Entities;
using PsnLib.Interfaces;
using PsnLib.Tools;

namespace PsnLib.Managers
{
    public class NotificationManager
    {
        private readonly IWebManager _webManager;

        public NotificationManager(IWebManager webManager)
        {
            _webManager = webManager;
        }

        public NotificationManager()
            : this(new WebManager())
        {
        }

        public async Task<NotificationEntity> GetNotifications(string username, UserAccountEntity userAccountEntity)
        {
            try
            {
                var user = userAccountEntity.GetUserEntity();
                var url = string.Format(EndPoints.Notification, user.Region, username, user.Language);
                var result = await _webManager.GetData(new Uri(url), userAccountEntity);
                var notification = JsonConvert.DeserializeObject<NotificationEntity>(result.ResultJson);
                return notification;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get notification list", ex);
            }
        }

        public async Task<bool> ClearNotification(NotificationEntity.Notification notification,
            UserAccountEntity userAccountEntity)
        {
            try
            {
                var user = userAccountEntity.GetUserEntity();
                var url = string.Format(EndPoints.ClearNotification, user.Region, user.OnlineId, notification.NotificationGroup, notification.NotificationId);
                var json = new StringContent("{\"seenFlag\":true}", Encoding.UTF8, "application/json");
                var result = await _webManager.PostData(new Uri(url), null, json, userAccountEntity);
                return result.IsSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to clear notification", ex);
            }
        }
    }
}
