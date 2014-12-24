using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using PsnLib.Entities;
using PsnLib.Interfaces;

namespace PsnLib.Managers
{
    public class WebManager : IWebManager
    {
        public bool IsNetworkAvailable => NetworkInterface.GetIsNetworkAvailable();

        public async Task<Result> PostData(Uri uri, MultipartContent header, StringContent content, UserAccountEntity userAccountEntity)
        {
            var httpClient = new HttpClient();
            try
            {
                var authenticationManager = new AuthenticationManager();
                if (userAccountEntity.GetAccessToken().Equals("refresh"))
                {
                    await authenticationManager.RefreshAccessToken(userAccountEntity);
                }
                var user = userAccountEntity.GetUserEntity();
                if (user != null)
                {
                    var language = userAccountEntity.GetUserEntity().Language;
                    httpClient.DefaultRequestHeaders.Add("Accept-Language", language);
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccountEntity.GetAccessToken());
                HttpResponseMessage response;
                if (header == null)
                {
                    response = await httpClient.PostAsync(uri, content);
                }
                else
                {
                    response = await httpClient.PostAsync(uri, header);
                }
                var responseContent = await response.Content.ReadAsStringAsync();
                return new Result(true, responseContent);
            }
            catch
            {
                // TODO: Add detail error result to json object.
                return new Result(false, string.Empty);
            }
        }

        public async Task<Result> PutData(Uri uri, StringContent json, UserAccountEntity userAccountEntity)
        {
            var httpClient = new HttpClient();
            try
            {
                var authenticationManager = new AuthenticationManager();
                if (userAccountEntity.GetAccessToken().Equals("refresh"))
                {
                    await authenticationManager.RefreshAccessToken(userAccountEntity);
                }
                var user = userAccountEntity.GetUserEntity();
                if (user != null)
                {
                    var language = userAccountEntity.GetUserEntity().Language;
                    httpClient.DefaultRequestHeaders.Add("Accept-Language", language);
                }
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccountEntity.GetAccessToken());
                var response = await httpClient.PutAsync(uri, json);
                var responseContent = await response.Content.ReadAsStringAsync();
                return response.IsSuccessStatusCode ? new Result(true, string.Empty) : new Result(false, responseContent);
            }
            catch (Exception)
            {

                return new Result(false, string.Empty);
            }
        }

        public async Task<Result> DeleteData(Uri uri, StringContent json, UserAccountEntity userAccountEntity)
        {
            var httpClient = new HttpClient();
            try
            {
                var authenticationManager = new AuthenticationManager();
                if (userAccountEntity.GetAccessToken().Equals("refresh"))
                {
                    await authenticationManager.RefreshAccessToken(userAccountEntity);
                }
                var user = userAccountEntity.GetUserEntity();
                if (user != null)
                {
                    var language = userAccountEntity.GetUserEntity().Language;
                    httpClient.DefaultRequestHeaders.Add("Accept-Language", language);
                }
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccountEntity.GetAccessToken());
                var response = await httpClient.DeleteAsync(uri);
                var responseContent = await response.Content.ReadAsStringAsync();
                return response.IsSuccessStatusCode ? new Result(true, string.Empty) : new Result(false, responseContent);
            }
            catch (Exception)
            {
                return new Result(false, string.Empty);
            }
        }

        public async Task<Result> GetData(Uri uri, UserAccountEntity userAccountEntity)
        {
            var httpClient = new HttpClient();
            try
            {
                var authenticationManager = new AuthenticationManager();
                if (userAccountEntity.GetAccessToken().Equals("refresh"))
                {
                    await authenticationManager.RefreshAccessToken(userAccountEntity);
                }
                var user = userAccountEntity.GetUserEntity();
                if (user != null)
                {
                    var language = userAccountEntity.GetUserEntity().Language;
                    httpClient.DefaultRequestHeaders.Add("Accept-Language", language);
                }
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccountEntity.GetAccessToken());
                var response = await httpClient.GetAsync(uri);
                var responseContent = await response.Content.ReadAsStringAsync();
                return string.IsNullOrEmpty(responseContent) ? new Result(false, string.Empty) : new Result(true, responseContent);
            }
            catch
            {
                // TODO: Add detail error result to json object.
                return new Result(false, string.Empty);
            }
        }

        public class Result
        {
            public Result(bool isSuccess, string json)
            {
                IsSuccess = isSuccess;
                ResultJson = json;
            }

            public bool IsSuccess { get; private set; }
            public string ResultJson { get; private set; }
        }
    }
}
