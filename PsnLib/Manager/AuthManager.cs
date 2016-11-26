using Newtonsoft.Json;
using PsnLib.Entities;
using PsnLib.Entities.Auth;
using PsnLib.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PsnLib.Manager
{
    public class AuthManager
    {
        /// <summary>
        /// Gets the NPSSO id. If the user has two step authentication on their account, it will return a ticket_uuid.
        /// In this case, you must run GetSsoCookieFromTwoFactorCodeAsync with the users Auth code and the ticketUuid
        /// in order to get the NPSSO.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>A results object with the type 'AuthResult`</returns>
        public async Task<Result> GetSsoCookieAsync(string username, string password)
        {
            using(var client = new HttpClient())
            {
                // Login Form.
                var loginFormCollection = new Dictionary<string, string>
                    {
                        {"authentication_type", "password"},
                        {"client_id", Oauth.ClientID},
                        {"username", username},
                        {"password", password},
                    };
                var loginForm = new FormUrlEncodedContent(loginFormCollection);

                // Send out initial request to get an "SSO Cookie", which is actually in JSON too. For some reason.
                // This could either be an SSO Cookie or a ticket_uuid, which is used for two step auth.
                var response = await client.PostAsync(EndPoints.SSOCookie, loginForm);

                var responseJson = await response.Content.ReadAsStringAsync();

                var result = new Result() { IsSuccess = response.IsSuccessStatusCode, ResultJson = responseJson };
                if (!result.IsSuccess)
                {
                    // If it's not a success, pass back the error to the user. Don't blow up,
                    // since their API does not directly show _why_ it blew up.
                    return ErrorHandler.CreateErrorObject(result, "Username/Password Failed", "Auth");
                }
                return result;
            }
        }

        /// <summary>
        /// Gets the NPSSO id from the user. Requires the auth code the user got via SMS and the ticketuuid from the
        /// previous step.
        /// </summary>
        /// <param name="code">The auth code the user got from SMS.</param>
        /// <param name="ticketuuid">The Ticket UUID gotten from GetSsoCookieAsync</param>
        /// <returns>A results object with the type 'AuthResult`</returns>
        public async Task<Result> GetSsoCookieFromTwoFactorCodeAsync(string code, string ticketuuid)
        {
            using (var client = new HttpClient())
            {
                // Login Form.
                var loginFormCollection = new Dictionary<string, string>
                    {
                        {"authentication_type", "two_step"},
                        {"client_id", Oauth.ClientID},
                        {"ticket_uuid", ticketuuid},
                        {"code", code},
                    };
                var loginForm = new FormUrlEncodedContent(loginFormCollection);

                // This should get the NPSSO string
                var response = await client.PostAsync(EndPoints.SSOCookie, loginForm);

                var responseJson = await response.Content.ReadAsStringAsync();

                var result = new Result() { IsSuccess = response.IsSuccessStatusCode, ResultJson = responseJson };
                if (!result.IsSuccess)
                {
                    // If it's not a success, pass back the error to the user. Don't blow up,
                    // since their API does not directly show _why_ it blew up.
                    return ErrorHandler.CreateErrorObject(result, "Two Factor Auth Failed", "Auth");
                }
                return result;
            }
        }

        /// <summary>
        /// Verify that the NPSSO the user recieved is valid. Not required for logging in but useful to check during
        /// the login process.
        /// </summary>
        /// <param name="NPSSO">The users NPSSO id.</param>
        /// <returns>A blank result object. If the key is valid, IsSuccess with return true.</returns>
        public async Task<Result> AuthorizeCheckAsync(string NPSSO)
        {
            using (var client = new HttpClient())
            {
                var authorizeCheck = new AuthCheck()
                {
                    Npsso = NPSSO,
                    ClientId = Oauth.ClientID,
                    Scope = Oauth.Scope,
                    ServiceEntity = Oauth.ServiceEntity
                };

                var json = JsonConvert.SerializeObject(authorizeCheck);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                // Call auth check so we can continue the flow...
                var result = await client.PostAsync(EndPoints.AuthorizeCheck, stringContent);
                return new Result() { IsSuccess = result.IsSuccessStatusCode };
            }
        }

        /// <summary>
        /// Get the auth code for the user. Used to get the tokens used for future requests.
        /// </summary>
        /// <param name="NPSSO">The NPSSO id for the user.</param>
        /// <returns>A results object containing the type 'CodeResult'</returns>
        public async Task<Result> GetAuthCodeAsync(string NPSSO)
        {
            var baseAddress = new Uri("https://auth.api.sonyentertainmentnetwork.com");
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(baseAddress, new Cookie("npsso", NPSSO));
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer, AllowAutoRedirect = false })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                var response = await client.GetAsync(EndPoints.CodeAuth);
                var codeUrl = response.Headers.Location.OriginalString;
                var queryString = UriExtensions.ParseQueryString(codeUrl.ToString());
                if (queryString.ContainsKey("authentication_error"))
                {
                    return ErrorHandler.CreateErrorObject(new Result(), "Failed to get OAuth Code (Authentication_error)", "Auth");
                }

                if (!queryString.ContainsKey("code"))
                {
                    return ErrorHandler.CreateErrorObject(new Result(), "Failed to get OAuth Code (No code)", "Auth");
                }

                return new Result() { IsSuccess = true, ResultJson = "{\"code\": \"" + queryString["code"] + "\"}" } ;
            }
        }

        /// <summary>
        /// Gets the access tokens for the user via auth code. This is the final step in the login process.
        /// </summary>
        /// <param name="code">The auth code gotten from GetAuthCodeAsync.</param>
        /// <param name="NPSSO">The users NPSSO id.</param>
        /// <returns>A results object with the type 'TokenResult'</returns>
        public async Task<Result> GetAccessTokenViaCodeAsync(string code, string NPSSO)
        {
            var baseAddress = new Uri("https://auth.api.sonyentertainmentnetwork.com");
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(baseAddress, new Cookie("npsso", NPSSO));
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer, AllowAutoRedirect = false })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                var dic = new Dictionary<string, string>
                {
                    ["grant_type"] = "authorization_code",
                    ["client_id"] = Oauth.ClientID,
                    ["client_secret"] = Oauth.ClientSecret,
                    ["redirect_uri"] = "com.playstation.PlayStationApp://redirect",
                    ["duid"] = Oauth.Duid,
                    ["scope"] = Oauth.Scope,
                    ["code"] = code,
                    ["service_entity"] = "urn:service-entity:psn"
                };
                var header = new FormUrlEncodedContent(dic);
                var response = await client.PostAsync(EndPoints.OauthToken, header);
                string responseContent = await response.Content.ReadAsStringAsync();
                return new Result(response.IsSuccessStatusCode, responseContent);
            }
        }

        /// <summary>
        /// Gets the access tokens for the user via refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token from the user.</param>
        /// <returns>A results object with the type 'TokenResult'</returns>
        public async Task<Result> RefreshTokensAsync(string refreshToken)
        {
            var baseAddress = new Uri("https://auth.api.sonyentertainmentnetwork.com");
            var cookieContainer = new CookieContainer();
            // We need to capture the redirect and steal the code from it.
            // Normally this would redirect to the users app, but it's hardcoded and we can't change it.
            // So instead, we'll give it back to the user and let them deal with it.
            using (var handler = new HttpClientHandler() { AllowAutoRedirect = false })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                var dic = new Dictionary<string, string>
                {
                    ["grant_type"] = "refresh_token",
                    ["client_id"] = Oauth.ClientID,
                    ["client_secret"] = Oauth.ClientSecret,
                    ["scope"] = Oauth.Scope,
                    ["refresh_token"] = refreshToken
                };
                var header = new FormUrlEncodedContent(dic);
                var response = await client.PostAsync(EndPoints.OauthToken, header);
                string responseContent = await response.Content.ReadAsStringAsync();
                return new Result(response.IsSuccessStatusCode, responseContent);
            }
        }
    }
}
