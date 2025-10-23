using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Sidh_Api.Repository
{
    public class TCListing
    {
        private readonly HttpClient _httpClient;
        private readonly SidhLogin _sidhLogin;
        public TCListing(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetSchemewiseTcListingAsync(string schemeid, int pageno, int itemperpage, string token)
        {
            var url = "https://uat.ekaushal.com/api/v1/stateIntegrationServiceInNode/getSchemewiseTcListing";

            var requestBody = new SchemeRequest
            {
                schemeId = schemeid,
                pageNo = pageno,
                itemsPerPage = itemperpage
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }


        public class SchemeRequest
        {
            public string? schemeId { get; set; }
            public int pageNo { get; set; }
            public int itemsPerPage { get; set; }
        }

        public class TcState
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class TcDistrict
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class TcLocation
        {
            public string longitude { get; set; }
            public string latitude { get; set; }
        }

        public class TcData
        {
            public string? tcId { get; set; }
            public string? tcName { get; set; }
            public string? tcType { get; set; }
            public string? tpUsername { get; set; }
            public string? tcAddress { get; set; }
            public TcState? tcState { get; set; }
            public TcDistrict? tcDistrict { get; set; }
            public TcLocation? tcLocation { get; set; }
            public DateTime? createdOn { get; set; }
            public string? email { get; set; }
        }

        public class SchemeResponse
        {

            public List<TcData> data { get; set; }


            public int statusCode { get; set; }


            public string status { get; set; }


            public Pagination pagination { get; set; }

        }
        public class Pagination
        {
            [JsonProperty("count")]
            public int Count { get; set; }

            [JsonProperty("limit")]
            public int Limit { get; set; }

            [JsonProperty("pageNo")]
            public int PageNo { get; set; }
        }
    }
}
