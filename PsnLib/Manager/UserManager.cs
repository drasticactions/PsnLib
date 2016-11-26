using PsnLib.Entities;
using PsnLib.Entities.User;
using PsnLib.Interfaces;
using PsnLib.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsnLib.Manager
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

        /// <summary>
        /// Gets complete information about a user. You can customise the response by setting the customFields string to
        /// return only specific fields if you don't require them all.
        /// </summary>
        /// <param name="userName">The requested username.</param>
        /// <param name="userAuthenticationTokens">The authentication tokens of the current user.</param>
        /// <param name="customFields">Custom, comma seperated, parameters you wish to search for. If empty, a default set will be used.</param>
        /// <param name="region">The region of the user, set to JA by default.</param>
        /// <param name="language">The language of the user, set to JA by default.</param>
        /// <returns>A results object set to type 'User'</returns>
        public async Task<Result> GetUserAsync(string userName, UserAuthenticationTokens userAuthenticationTokens, string customFields = "",
            string region = "jp", string language = "ja")
        {
            try
            {
                var url = string.IsNullOrEmpty(customFields) ? string.Format(EndPoints.UserDefault, region, userName) : string.Format(EndPoints.User, region, userName, customFields);
                return await _webManager.GetDataAsync(new Uri(url), userAuthenticationTokens, language);
            }
            catch (Exception exception)
            {
                throw new Exception("Error getting user", exception);
            }
        }

        /// <summary>
        /// Gets the avatar of the user requested.
        /// </summary>
        /// <param name="userName">The requested username.</param>
        /// <param name="userAuthenticationTokens">The authentication tokens of the current user.</param>
        /// <param name="region">The region of the user, set to JA by default.</param>
        /// <param name="language">The language of the user, set to JA by default.</param>
        /// <returns>A results object set to type 'User'</returns>
        public async Task<Result> GetUserAvatarAsync(string userName, UserAuthenticationTokens userAuthenticationTokens,
            string region = "jp", string language = "ja")
        {
            try
            {
                var url = string.Format(EndPoints.UserAvatars, region, userName);
                return await _webManager.GetDataAsync(new Uri(url), userAuthenticationTokens, language);
            }
            catch (Exception exception)
            {
                throw new Exception("Error getting user avatars", exception);
            }
        }
    }
}
