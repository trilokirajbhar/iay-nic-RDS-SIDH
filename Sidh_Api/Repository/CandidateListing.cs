using Newtonsoft.Json;
using Sidh_Api.DTO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Sidh_Api.Repository.TCListing;

namespace Sidh_Api.Repository
{
    public class CandidateListing
    {
        private readonly HttpClient _httpClient;

        public CandidateListing(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<string> GetCandidateListingAsync(int pageno, int itemperpage, string schemeid, string token)
        {
            var url = "https://uat.ekaushal.com/api/v1/stateIntegrationServiceInNode/getSchemewiseCandidateListing";

            var requestBody = new CandidateResponseDTO
            {
             pageNo=pageno,
             itemsPerPage= itemperpage,
             schemeId= schemeid
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
    }
}
