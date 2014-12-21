using System;
using System.Net.Http;
using System.Threading.Tasks;
using PsnLib.Entities;
using PsnLib.Managers;

namespace PsnLib.Interfaces
{
    public interface IWebManager
    {
        bool IsNetworkAvailable { get; }
        Task<WebManager.Result> PostData(Uri uri, MultipartContent header, StringContent content, UserAccountEntity userAccountEntity);

        Task<WebManager.Result> PutData(Uri uri, StringContent json, UserAccountEntity userAccountEntity);

        Task<WebManager.Result> DeleteData(Uri uri, StringContent json, UserAccountEntity userAccountEntity);

        Task<WebManager.Result> GetData(Uri uri, UserAccountEntity userAccountEntity);
    }
}
