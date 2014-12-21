using System.Threading.Tasks;
using PsnLib.Entities;
using PsnLib.Tools;

namespace PsnLib.Interfaces
{
    public interface IAuthenticationManager
    {
        string Status { get; }

        Task<UserAccountEntity> Authenticate(string userName, string password,
            int timeout = EndPoints.DefaultTimeoutInMilliseconds);
    }
}
