using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sidh_Api.Database;
using Sidh_Api.DTO;
using Sidh_Api.Models;
using Sidh_Api.Repository;
using System.Text;
using static Sidh_Api.Repository.SchemeListing;
using static SidhLogin;


namespace Sidh_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchemeListDataController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly SidhLogin _sidhLogin;
        private readonly SchemeListing _schemelising;
        private readonly MoprContext _moprContext;
        //private readonly IHttpClientFactory _httpClientFactory;


        public SchemeListDataController(HttpClient httpClient, SidhLogin sidhLogin, SchemeListing schemeListing, MoprContext moprContext)
        {
            _httpClient = httpClient;
            _sidhLogin = sidhLogin;
            _schemelising = schemeListing;
            _moprContext = moprContext;
            //_httpClientFactory = httpClientFactory;

        }
        [HttpPost("Fetchscheme")]
        public async Task<IActionResult> Fetchscheme(SchemeRequests request)
        {
            SchemeResponse schemeliresponse = new SchemeResponse();
            CsrfResponse tokens = new CsrfResponse();
            var token = await LoginAsync();
            tokens = JsonConvert.DeserializeObject<CsrfResponse>(token);
            var apiResponse = await _schemelising.GetSchemeListingAsync(request.pageNo, request.itemsPerPage, tokens.Token);
            schemeliresponse = JsonConvert.DeserializeObject<SchemeResponse>(apiResponse);
            if (schemeliresponse.data.Count == 0)
            {
                return BadRequest("No data returned from API");
            }
            foreach (var item in schemeliresponse.data)
            {
                var record = new TrainingScheme
                {
                    SchemeId = item.schemeId,
                    SchemeName = item.schemeName,
                    BasicschemeId = item.basicDetails.schemeName.id,
                    BasicschemeName = item.basicDetails.schemeName.name,
                    TrainingType = item.basicDetails.trainingType.name,
                    SubSchemeName = item.basicDetails.subSchemeName.name,
                    SchemeReferenceType = item.basicDetails?.schemeReferenceType?.name,
                    SchemeWorkflowName = item.basicDetails?.schemeWorkflow?.name,
                    SchemeWorkflowDiscrip = item.basicDetails?.schemeWorkflow?.description,
                    SchemeWorkflowId = item.basicDetails?.schemeWorkflow?.id,
                    RulesEffectiveFrom = item.rulesEffectiveFrom,
                    RulesEffectiveTo = item.rulesEffectiveTo,
                    InitiativeOf = item.initiativeOf,
                    ProgramBy = item.programBy,
                    SidSchemeDisplayName = item.sidSchemeDisplayName,
                    Schemeworkflownames = item.schemeWorkflowName,
                    IsQpLinkedScheme = item.isQPLinkedScheme,
                    SidSchemeName = item.sidSchemeName
                };

                _moprContext.TrainingSchemes.Add(record);
            }

            await _moprContext.SaveChangesAsync();

            return Ok(new { message = "Data saved successfully", count = schemeliresponse.data.Count });
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
    }
}
