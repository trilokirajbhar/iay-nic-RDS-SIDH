using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sidh_Api.Database;
using Sidh_Api.Models;
using Sidh_Api.Repository;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using static Sidh_Api.Repository.TCListing;
using static SidhLogin;

namespace Sidh_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TCListingController : ControllerBase
    {

        private readonly HttpClient _httpClient;
        private readonly SidhLogin _sidhLogin;
        private readonly TCListing _tclising;
        private readonly MoprContext _moprContext;
        private readonly IHttpClientFactory _httpClientFactory;


        public TCListingController(HttpClient httpClient, SidhLogin sidhLogin, TCListing tCListing, MoprContext moprContext, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClient;
            _sidhLogin = sidhLogin;
            _tclising = tCListing;
            _moprContext = moprContext;
            _httpClientFactory = httpClientFactory;

        }


        [HttpPost("fetch-and-save")]
        public async Task<IActionResult> FetchAndSaveData(SchemeRequest request)
        {
            SchemeResponse tcresponse = new SchemeResponse();
            CsrfResponse tokens = new CsrfResponse();
            //var csrf = await CSRFToken();
            var token = await LoginAsync();
            tokens = JsonConvert.DeserializeObject<CsrfResponse>(token);
            var apiResponse = await _tclising.GetSchemewiseTcListingAsync(request.schemeId, request.pageNo, request.itemsPerPage, tokens.Token);
            tcresponse = JsonConvert.DeserializeObject<SchemeResponse>(apiResponse);
            if (tcresponse.data.Count == 0)
            {
                return BadRequest("No data returned from API");
            }
            foreach (var item in tcresponse.data)
            {
                var record = new TrainingCenter
                {
                    TcId = item.tcId,
                    TcName = item.tcName,
                    TcType = item.tcType,
                    TpUsername = item.tpUsername,
                    TcAddress = item.tcAddress,
                    StateId = item.tcState.id,
                    StateName = item.tcState.name,
                    DistrictId = item.tcDistrict.id,
                    DistrictName = item.tcDistrict.name,
                    Longitude = item.tcLocation.longitude,
                    Latitude = item.tcLocation.latitude,
                    CreatedOn = item.createdOn,
                    Email = item.email
                };

                _moprContext.TrainingCenters.Add(record);
            }

            await _moprContext.SaveChangesAsync();

            return Ok(new { message = "Data saved successfully", count = tcresponse.data.Count });
        }

        [NonAction]
        public async Task<string> LoginAsync()
        {
            var url = "https://uat.ekaushal.com/api/user/v1/login";
            var payload = new
            {
                userName = "ExternalService_03_RDS",
                password = "RBrl6uYjL5ZY0aLDMvIKnz0Wf8apt/5LBQwq35UmQaKEbboH6abgNJWUD5W9oEMojlZZEP7f5sk+i7NA0vpCGZLs/uIkpBVLBQvMQDBVoiSYth875/BZVzLFxbxrwz+bd2s34GiHw49b3NihxCUImvqJL/CyRYWaOiWIgBWIjuQ=tDyt2"
            };

            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-Csrf-Token", await _sidhLogin.GetTokenAsync());
            var response = await _httpClient.PostAsync(url, content);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error: {response.StatusCode} - {error}");
            }
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        public class loginResponse
        {
            public string loginToken { get; set; }
        }
    }

}
