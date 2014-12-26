using Newtonsoft.Json;
using PsnLib.Entities.SearchEntities;

namespace PsnLib.Entities
{
    public class SearchResultsEntity
    {
        [JsonProperty("searchResults")]
        public SearchResult[] SearchResults { get; set; }

        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("totalResults")]
        public int TotalResults { get; set; }
    }
}
