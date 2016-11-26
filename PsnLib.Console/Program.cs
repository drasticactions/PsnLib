using Newtonsoft.Json;
using PsnLib.Entities.Auth;
using PsnLib.Entities.User;
using PsnLib.Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsnLib.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var authManager = new AuthManager();
            var cookieResult = await authManager.GetSsoCookieAsync("", "");
            var authResult = JsonConvert.DeserializeObject<AuthResult>(cookieResult.ResultJson);
            System.Console.WriteLine(cookieResult.ResultJson);
            var authCheckResult = await authManager.AuthorizeCheckAsync(authResult.NPSSO);
            var test = await authManager.GetAuthCodeAsync(authResult.NPSSO);
            var codeResult = JsonConvert.DeserializeObject<CodeResult>(test.ResultJson);
            var tokens = await authManager.GetAccessTokenViaCodeAsync(codeResult.Code, authResult.NPSSO);
            var tokenResult = JsonConvert.DeserializeObject<UserAuthenticationTokens>(tokens.ResultJson);
            var test2 = await authManager.RefreshTokensAsync(tokenResult.RefreshToken);
         }
    }
}
