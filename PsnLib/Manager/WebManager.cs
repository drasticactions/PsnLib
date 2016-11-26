using Newtonsoft.Json;
using PsnLib.Entities;
using PsnLib.Entities.Auth;
using PsnLib.Entities.User;
using PsnLib.Interfaces;
using PsnLib.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PsnLib.Manager
{
    public class WebManager : IWebManager
    {
        public async Task<Result> PutDataAsync(Uri uri, StringContent json, UserAuthenticationTokens userAuthenticationEntity, string language = "ja")
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            using (var httpClient = new HttpClient(handler))
            {
                try
                {
                    var authenticationManager = new AuthManager();
                    Result result = new Result(false, "");
                    if (RefreshTime(userAuthenticationEntity.ExpiresInDate))
                    {
                        var tokens = await authenticationManager.RefreshTokensAsync(userAuthenticationEntity.RefreshToken);
                        userAuthenticationEntity = JsonConvert.DeserializeObject<UserAuthenticationTokens>(tokens.ResultJson);
                        result.Tokens = tokens.ResultJson;
                    }
                    httpClient.DefaultRequestHeaders.Add("Accept-Language", language);
                    httpClient.DefaultRequestHeaders.Add("Origin", "http://psapp.dl.playstation.net");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAuthenticationEntity.AccessToken);
                    var response = await httpClient.PutAsync(uri, json);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    result.IsSuccess = response.IsSuccessStatusCode;
                    result.ResultJson = responseContent;
                    return result;
                }
                catch (Exception ex)
                {
                    return ErrorHandler.CreateErrorObject(new Result(), ex.Message, "PutDataAsync");
                }
            }
        }

        public async Task<Result> DeleteDataAsync(Uri uri, StringContent json, UserAuthenticationTokens userAuthenticationEntity, string language = "ja")
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            using (var httpClient = new HttpClient(handler))
            {
                try
                {
                    var authenticationManager = new AuthManager();
                    Result result = new Result(false, "");
                    if (RefreshTime(userAuthenticationEntity.ExpiresInDate))
                    {
                        var tokens = await authenticationManager.RefreshTokensAsync(userAuthenticationEntity.RefreshToken);
                        userAuthenticationEntity = JsonConvert.DeserializeObject<UserAuthenticationTokens>(tokens.ResultJson);
                        result.Tokens = tokens.ResultJson;
                    }
                    httpClient.DefaultRequestHeaders.Add("Accept-Language", language);
                    httpClient.DefaultRequestHeaders.Add("Origin", "http://psapp.dl.playstation.net");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAuthenticationEntity.AccessToken);
                    var response = await httpClient.DeleteAsync(uri);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    result.IsSuccess = response.IsSuccessStatusCode;
                    result.ResultJson = responseContent;
                    return result;
                }
                catch (Exception ex)
                {
                    return ErrorHandler.CreateErrorObject(new Result(), ex.Message, "DeleteDataAsync");
                }
            }
        }

        public async Task<Result> PostDataAsync(Uri uri, StringContent content, UserAuthenticationTokens userAuthenticationEntity, string language = "ja", bool isMessage = false)
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            using (var httpClient = new HttpClient(handler))
            {
                try
                {
                    var authenticationManager = new AuthManager();
                    Result result = new Result(false, "");
                    if (RefreshTime(userAuthenticationEntity.ExpiresInDate))
                    {
                        var tokens = await authenticationManager.RefreshTokensAsync(userAuthenticationEntity.RefreshToken);
                        userAuthenticationEntity = JsonConvert.DeserializeObject<UserAuthenticationTokens>(tokens.ResultJson);
                        result.Tokens = tokens.ResultJson;
                    }
                    if (isMessage)
                    {
                        httpClient.DefaultRequestHeaders.Add("User-Agent", "PlayStation Messages App/3.20.23");
                    }
                    httpClient.DefaultRequestHeaders.Add("Accept-Language", language);
                    httpClient.DefaultRequestHeaders.Add("Origin", "http://psapp.dl.playstation.net");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAuthenticationEntity.AccessToken);
                    var response = await httpClient.PostAsync(uri, content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    result.IsSuccess = response.IsSuccessStatusCode;
                    result.ResultJson = responseContent;
                    return result;
                }
                catch (Exception ex)
                {
                    return ErrorHandler.CreateErrorObject(new Result(), ex.Message, "PostDataAsync");
                }
            }
        }

        public async Task<Result> PostDataAsync(Uri uri, FormUrlEncodedContent header, UserAuthenticationTokens userAuthenticationEntity, string language = "ja", bool isMessage = false)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var authenticationManager = new AuthManager();
                    Result result = new Result(false, "");
                    if (RefreshTime(userAuthenticationEntity.ExpiresInDate))
                    {
                        var tokens = await authenticationManager.RefreshTokensAsync(userAuthenticationEntity.RefreshToken);
                        userAuthenticationEntity = JsonConvert.DeserializeObject<UserAuthenticationTokens>(tokens.ResultJson);
                        result.Tokens = tokens.ResultJson;
                    }
                    if (isMessage)
                    {
                        httpClient.DefaultRequestHeaders.Add("User-Agent", "PlayStation Messages App/3.20.23");
                    }
                    httpClient.DefaultRequestHeaders.Add("Accept-Language", language);
                    httpClient.DefaultRequestHeaders.Add("Origin", "http://psapp.dl.playstation.net");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAuthenticationEntity.AccessToken);
                    var response = await httpClient.PostAsync(uri, header);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    result.IsSuccess = response.IsSuccessStatusCode;
                    result.ResultJson = responseContent;
                    return result;
                }
                catch (Exception ex)
                {
                    return ErrorHandler.CreateErrorObject(new Result(), ex.Message, "PostDataAsync");
                }
            }
        }

        public async Task<Result> GetDataAsync(Uri uri, UserAuthenticationTokens userAuthenticationEntity, string language = "ja")
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var authenticationManager = new AuthManager();
                    Result result = new Result(false, "");
                    if (RefreshTime(userAuthenticationEntity.ExpiresInDate))
                    {
                        var tokens = await authenticationManager.RefreshTokensAsync(userAuthenticationEntity.RefreshToken);
                        userAuthenticationEntity = JsonConvert.DeserializeObject<UserAuthenticationTokens>(tokens.ResultJson);
                        result.Tokens = tokens.ResultJson;
                    }
                    httpClient.DefaultRequestHeaders.Add("Origin", "http://psapp.dl.playstation.net");
                    httpClient.DefaultRequestHeaders.Add("Accept-Language", language);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAuthenticationEntity.AccessToken);
                    var response = await httpClient.GetAsync(uri);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    result.IsSuccess = response.IsSuccessStatusCode;
                    result.ResultJson = responseContent;
                    return result;
                }
                catch (Exception ex)
                {
                    return ErrorHandler.CreateErrorObject(new Result(), ex.Message, "GetDataAsync");
                }
            }
        }

        public async Task<Result> PostDataAsync(Uri uri, MultipartContent content, UserAuthenticationTokens userAuthenticationEntity, string language = "ja", bool isMessage = false)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var authenticationManager = new AuthManager();
                    Result result = new Result(false, "");
                    if (RefreshTime(userAuthenticationEntity.ExpiresInDate))
                    {
                        var tokens = await authenticationManager.RefreshTokensAsync(userAuthenticationEntity.RefreshToken);
                        userAuthenticationEntity = JsonConvert.DeserializeObject<UserAuthenticationTokens>(tokens.ResultJson);
                        result.Tokens = tokens.ResultJson;
                    }
                    if (isMessage)
                    {
                        httpClient.DefaultRequestHeaders.Add("User-Agent", "PlayStation Messages App/3.20.23");
                    }
                    httpClient.DefaultRequestHeaders.Add("Accept-Language", language);
                    httpClient.DefaultRequestHeaders.Add("Origin", "http://psapp.dl.playstation.net");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAuthenticationEntity.AccessToken);
                    var response = await httpClient.PostAsync(uri, content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    result.IsSuccess = response.IsSuccessStatusCode;
                    result.ResultJson = responseContent;
                    return result;
                }
                catch (Exception ex)
                {
                    return ErrorHandler.CreateErrorObject(new Result(), ex.Message, "PostDataAsync");
                }
            }
        }

        private bool RefreshTime(long refreshTime)
        {
            return AuthHelpers.GetUnixTime(DateTime.Now) > refreshTime;
        }
    }
}
