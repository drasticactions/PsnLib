using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsnLib.Tools
{
    // Oauth Keys taken from the iOS app.
    public class Oauth
    {
        public const string ClientID = "4db3729d-4591-457a-807a-1cf01e60c3ac";

        public const string ClientSecret = "criemouwIuVoa4iU";

        public const string ClientIdSecretBase64 = "NGRiMzcyOWQtNDU5MS00NTdhLTgwN2EtMWNmMDFlNjBjM2FjOmNyaWVtb3V3SXVWb2E0aVU=";

        public const string Scope = "psn:sceapp,user:account.get,user:account.settings.privacy.get,user:account.settings.privacy.update,user:account.realName.get,user:account.realName.update,kamaji:get_account_hash,kamaji:ugc:distributor,oauth:manage_device_usercodes,kamaji:game_list,capone:report_submission,kamaji:get_internal_entitlements";

        public const string ServiceEntity = "urn:service-entity:psn";

        public const string Duid = "0000000d0004008004DD566E642649FA9C97D2EB5E2B90E0";
    }
    public class EndPoints
    {
        #region SSO

        public const string SSOCookie = "https://auth.api.sonyentertainmentnetwork.com/2.0/ssocookie";

        public const string AuthorizeCheck = "https://auth.api.sonyentertainmentnetwork.com/2.0/oauth/authorizeCheck";

        public const string OauthToken = "https://auth.api.sonyentertainmentnetwork.com/2.0/oauth/token";

        public const string CodeAuth = "https://auth.api.sonyentertainmentnetwork.com/2.0/oauth/authorize?state=HuZOQifegn406XO1rGfaxqT57I4&ui=pr&duid=0000000d0004008004DD566E642649FA9C97D2EB5E2B90E0&app_context=inapp_ios&client_id=4db3729d-4591-457a-807a-1cf01e60c3ac&device_base_font_size=10&device_profile=mobile&redirect_uri=com.playstation.PlayStationApp://redirect&response_type=code&scope=psn:sceapp,user:account.get,user:account.settings.privacy.get,user:account.settings.privacy.update,user:account.realName.get,user:account.realName.update,kamaji:get_account_hash,kamaji:ugc:distributor,oauth:manage_device_usercodes,kamaji:game_list,capone:report_submission,kamaji:get_internal_entitlements&service_entity=urn:service-entity:psn&service_logo=ps&smcid=psapp:signin&support_scheme=sneiprls";

        #endregion

        #region User

        public const string UserAvatars = "https://{0}-prof.np.community.playstation.net/userProfile/v1/users/{1}/profile?fields=avatarUrls";

        public const string UserDefault = "https://{0}-prof.np.community.playstation.net/userProfile/v1/users/{1}/profile?fields=onlineId,aboutMe,languagesUsed,plus,@personalDetail,avatarUrls,presence,isOfficiallyVerified,relation,requestMessageFlag,trophySummary,npTitleIconUrl,mutualFriendsCount&avatarSizes=m,xl&profilePictureSizes=m,xl&languagesUsedLanguageSet=set3&psVitaTitleIcon=circled&titleIconSize=s&aboutMeType=1";

        public const string User = "https://{0}-prof.np.community.playstation.net/userProfile/v1/users/{1}/profile?fields={2}";

        #endregion
    }
}
