using PsnLib.Entities;
using PsnLib.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PsnLib.Interfaces
{
    public interface IWebManager
    {
        Task<Result> PutDataAsync(Uri uri, StringContent json, UserAuthenticationTokens userAuthenticationEntity, string language = "ja");

        Task<Result> DeleteDataAsync(Uri uri, StringContent json, UserAuthenticationTokens userAuthenticationEntity, string language = "ja");

        Task<Result> PostDataAsync(Uri uri, StringContent content, UserAuthenticationTokens userAuthenticationEntity, string language = "ja", bool isMessage = false);

        Task<Result> PostDataAsync(Uri uri, FormUrlEncodedContent header, UserAuthenticationTokens userAuthenticationEntity, string language = "ja", bool isMessage = false);

        Task<Result> GetDataAsync(Uri uri, UserAuthenticationTokens userAuthenticationEntity, string language = "ja");
        Task<Result> PostDataAsync(Uri uri, MultipartContent content, UserAuthenticationTokens userAuthenticationEntity, string language = "ja", bool isMessage = false);
    }
}
