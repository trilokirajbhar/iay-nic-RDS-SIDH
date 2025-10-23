using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sidh_Api.Database;
using Sidh_Api.DTO;
using Sidh_Api.Models;
using Sidh_Api.Repository;
using System.Text;
using static Sidh_Api.Repository.BatchListing;
using static SidhLogin;


namespace Sidh_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchListingController : ControllerBase
    {
        private readonly BatchListing _batchlisting;
        private readonly HttpClient _httpClient;
        private readonly SidhLogin _sidhLogin;
        private readonly MoprContext _moprContext;


        public BatchListingController(BatchListing batchlisting, MoprContext moprContext, HttpClient httpClient, SidhLogin sidhLogin)
        {
            _batchlisting = batchlisting;
            _moprContext = moprContext;
            _httpClient = httpClient;
            _sidhLogin = sidhLogin;
        }

        [HttpPost("fetch")]
        public async Task<IActionResult> FetchAndSave(BatchDTO batchDTO)
        {

            CsrfResponse tokens = new CsrfResponse();
            var token = await LoginAsync();
            tokens = JsonConvert.DeserializeObject<CsrfResponse>(token);
           var datas= await _batchlisting.FetchAndSaveBatchesAsync(batchDTO.pageNo, batchDTO.itemsPerPage, batchDTO.schemeId, batchDTO.fromDate, tokens.Token);
            var apiResponse= JsonConvert.DeserializeObject<BatchApiDTO>(datas);

            if (apiResponse?.data != null)
            {
                foreach (var b in apiResponse.data)
                {
                    // Map API data to Entity
                    var batch = new Batch
                    {
                        BatchId = b.batchId.ToString(),
                        BatchName = b.batchName,
                        BatchStartDate = b.batchStartDate,
                        BatchEndDate = b.batchEndDate,
                        BatchSize = b.batchSize,
                        BatchStage = b.batchStage,
                        TcId = b.tcId,
                        TcName = b.tcName,
                        TcLongitude = b.tcLongitude,
                        TcLatitude = b.tcLatitude,
                        TcSpocName = b.tcSpocName,
                        TcSpocMobile = b.tcSpocMobile,
                        TcSpocEmail = b.tcSpocEmail,
                        TcAddressLine = b.tcAddressLine,
                        Pincode = b.pincode,
                        TpId = b.tpId,
                        TpName = b.tpName,
                        SchemeName = b.schemeName,
                        SchemeId = b.schemeID,
                        CreatedAt = DateTime.UtcNow
                    };

                    // Job roles
                    if (b.jobRoles != null)
                    {
                        foreach (var jr in b.jobRoles)
                        {
                            batch.BatchJobRoles.Add(new BatchJobRole
                            {
                                BatchId = b.batchId.ToString(),
                                JobName = jr.jobName,
                                QpCode = jr.qpCode,
                                Version = jr.version,
                                NsqfLevel = jr.nsqfLevel,
                                JobRoleDesc = jr.jobRoleDesc,
                                SectorId = jr.sectorId,
                                SectorName = jr.sectorName
                            });
                        }
                    }

                    // Save to DB
                    _moprContext.Batches.Add(batch);
                }

                await _moprContext.SaveChangesAsync();
            }

            return Ok("Batch data fetched and saved successfully!");
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
