using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sidh_Api.Database;
using Sidh_Api.DTO;
using Sidh_Api.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Sidh_Api.Repository
{
    public class BatchListing
    {
        private readonly HttpClient _httpClient;
        private readonly MoprContext _moprContext;

        public BatchListing(HttpClient httpClient, MoprContext context)
        {
            _httpClient = httpClient;
            _moprContext = context;
        }


        public async Task<string> FetchAndSaveBatchesAsync(int pageno, int itemperpage, string schemeid,string fromDate, string token)
        {
            var url = "https://uat.ekaushal.com/api/v1/stateIntegrationServiceInNode/getSchemewiseBatchListing";
             

            var requestBody = new BatchDTO
            {
                pageNo = pageno,
                itemsPerPage = itemperpage,
                schemeId = schemeid,
                fromDate = fromDate
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
