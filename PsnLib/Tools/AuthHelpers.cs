using PsnLib.Entities.Auth;
using PsnLib.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsnLib.Tools
{
    public class AuthHelpers
    {
        public static long GetUnixTime(DateTime time)
        {
            time = time.ToUniversalTime();
            TimeSpan timeSpam = time - (new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local));
            return (long)timeSpam.TotalSeconds;
        }
    }
}
