using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Sidh_Api.Repository.TCListing;


namespace Sidh_Api.Repository
{
    public class SchemeListing
    {
        private readonly HttpClient _httpClient;
        //private readonly SidhLogin _sidhLogin;
        public SchemeListing(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetSchemeListingAsync(int pageno, int itemperpage, string token)
        {
            var url = "https://uat.ekaushal.com/api/v1/schemeModule/scheme/getSchemeListing";

            var requestBody = new SchemeRequest
            {
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


        //public class SchemeRequest
        //{
        //    public int pageNo { get; set; }
        //    public int itemsPerPage { get; set; }
        //}

        public class SchemeResponse
        {
            public List<SchemeData>? data { get; set; }
            public int statusCode { get; set; }
            public string? status { get; set; }
            public Pagination? Pagination { get; set; }
        }

        public class SchemeData
        {
            public string? schemeId { get; set; }
            public string? schemeName { get; set; }
            public basicDetails? basicDetails { get; set; }
            public string? rulesEffectiveFrom { get; set; }
            public string? rulesEffectiveTo { get; set; }
            public string? initiativeOf { get; set; }
            public string? programBy { get; set; }
            public string? sidSchemeDisplayName { get; set; }
            public string? schemeWorkflowName { get; set; }
            public bool isQPLinkedScheme { get; set; }
            public string? sidSchemeName { get; set; }
        }

        public class basicDetails
        {
            public schemeName? schemeName { get; set; }
            public trainingType? trainingType { get; set; }
            public subSchemeName? subSchemeName { get; set; }
            public schemeReferenceType? schemeReferenceType { get; set; }
            public schemeWorkflow? schemeWorkflow { get; set; }
        }

        public class schemeName
        {
            public string id { get; set; }
            public string? name { get; set; }
        }

        public class trainingType
        {
            public string? name { get; set; }
        }

        public class subSchemeName
        {
            public string? name { get; set; }
        }

        public class schemeReferenceType
        {
            public string? name { get; set; }
        }

        public class schemeWorkflow
        {
            public string? name { get; set; }
            public string? description { get; set; }
            public int id { get; set; }
        }

        //public class Pagination
        //{
        //    public int count { get; set; }
        //    public int limit { get; set; }
        //    public int pageNo { get; set; }
        //}

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
