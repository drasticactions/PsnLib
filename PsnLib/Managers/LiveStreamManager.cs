using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PsnLib.Entities;
using PsnLib.Interfaces;
using PsnLib.Tools;

namespace PsnLib.Managers
{
    public class LiveStreamManager
    {
        private readonly IWebManager _webManager;

        public LiveStreamManager(IWebManager webManager)
        {
            _webManager = webManager;
        }

        public LiveStreamManager()
            : this(new WebManager())
        {
        }

        public async Task<NicoNicoEntity> GetNicoFeed(string status, string platform, int offset, int limit, string sort,
            UserAccountEntity userAccountEntity)
        {
            try
            {
                var url = EndPoints.NicoNicoBaseUrl;
                // Sony's app hardcodes this value to 0. 
                // This app could, in theory, allow for more polling of data, so these options are left open to new values and limits.
                url += string.Format("offset={0}&", offset);
                url += string.Format("limit={0}&", limit);
                url += string.Format("status={0}&", status);
                url += string.Format("sce_platform={0}&", platform);
                url += string.Format("sort={0}", sort);
                // TODO: Fix this cheap hack to get around caching issue. For some reason, no-cache is not working...
                url += "&r=" + Guid.NewGuid();
                var result = await _webManager.GetData(new Uri(url), userAccountEntity);
                var nico = JsonConvert.DeserializeObject<NicoNicoEntity>(result.ResultJson);
                return nico;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get Nico Nico Douga feed", ex);
            }
        }

        public async Task<TwitchEntity> GetTwitchFeed(int offset, int limit, string platform,
            string titlePreset, string query, UserAccountEntity userAccountEntity)
        {
            try
            {
                var url = EndPoints.TwitchBaseUrl;
                // Sony's app hardcodes this value to 0. 
                // This app could, in theory, allow for more polling of data, so these options are left open to new values and limits.
                url += string.Format("offset={0}&", offset);
                url += string.Format("limit={0}&", limit);
                url += string.Format("query={0}&", query);
                var result = await _webManager.GetData(new Uri(url), userAccountEntity);
                var twitch = JsonConvert.DeserializeObject<TwitchEntity>(result.ResultJson);
                return twitch;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get Twitch feed", ex);
            }
        }

        public async Task<UstreamEntity> GetUstreamFeed(int pageNumber, int pageSize, string detailLevel,
            Dictionary<string, string> filterList, string sortBy, string query, UserAccountEntity userAccountEntity)
        {
            try
            {
                var url = EndPoints.UstreamBaseUrl;
                url += string.Format("p={0}&", pageNumber);
                url += string.Format("pagesize={0}&", pageSize);

                url += string.Format("detail_level={0}&", detailLevel);
                foreach (var item in filterList)
                {
                    if (item.Key.Equals(EndPoints.UstreamUrlConstants.Interactive))
                    {
                        url += string.Format(EndPoints.UstreamUrlConstants.FilterBase, EndPoints.UstreamUrlConstants.PlatformPs4) +
                                         "[interactive]=" + item.Value + "&";
                    }
                    else
                    {
                        url += string.Concat(string.Format(EndPoints.UstreamUrlConstants.FilterBase, item.Key), "=", item.Value + "&");
                    }
                }
                url += string.Format("sort={0}", sortBy);
                url += "&r=" + Guid.NewGuid();
                var result = await _webManager.GetData(new Uri(url), userAccountEntity);
                var ustream = JsonConvert.DeserializeObject<UstreamEntity>(result.ResultJson);
                return ustream;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get Ustream feed", ex);
            }
        }
    }
}
