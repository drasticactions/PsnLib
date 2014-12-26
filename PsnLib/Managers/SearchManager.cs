using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PsnLib.Entities;
using PsnLib.Interfaces;
using PsnLib.Tools;

namespace PsnLib.Managers
{
    public class SearchManager
    {
        private readonly IWebManager _webManager;

        public SearchManager(IWebManager webManager)
        {
            _webManager = webManager;
        }

        public SearchManager()
            : this(new WebManager())
        {
        }

        public async Task<SearchResultsEntity> SearchForUsers(int offset, string query, UserAccountEntity userAccountEntity)
        {
            try
            {
                var url = string.Format(EndPoints.Search, offset, query);
                url += "&r=" + Guid.NewGuid();
                var result = await _webManager.GetData(new Uri(url), userAccountEntity);
                var search = JsonConvert.DeserializeObject<SearchResultsEntity>(result.ResultJson);
                return search;
            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message, ex);
            }
        }
    }
}
