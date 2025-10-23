using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sidh_Api.Database;
using Sidh_Api.DTO;
using Sidh_Api.Models;
using Sidh_Api.Repository;
using System;
using System.Net.Http.Headers;
using System.Text;
using static SidhLogin;

namespace Sidh_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateListingController : ControllerBase
    {

        private readonly HttpClient _httpClient;
        private readonly SidhLogin _sidhLogin;
        private readonly MoprContext _moprContext;
        private readonly CandidateListing _candidatelisting;


        public CandidateListingController(MoprContext context, HttpClient httpClient, CandidateListing candidatelisting, SidhLogin sidhLogin)
        {
            _moprContext = context;
            _httpClient = httpClient;
            _candidatelisting = candidatelisting;
            _sidhLogin = sidhLogin;
        }


        [HttpPost("candidatelisting")]
        public async Task<IActionResult> CandidateListing(CandidateResponseDTO candidateresponseDTO)
        {
            CsrfResponse tokens = new CsrfResponse();
            var token = await LoginAsync();
            tokens = JsonConvert.DeserializeObject<CsrfResponse>(token);
            var datas = await _candidatelisting.GetCandidateListingAsync(candidateresponseDTO.pageNo, candidateresponseDTO.itemsPerPage, candidateresponseDTO.schemeId, tokens.Token);
            var candidresponse = JsonConvert.DeserializeObject<CandidateResponseDto>(datas);

            if (candidresponse?.data != null)
            {
                foreach (var item in candidresponse.data)
                {
                    var candidate = new Candidate
                    {
                        Candidateid = item.candidatePersonalDetails.candidateId,
                        Candidatename = item.candidatePersonalDetails.candidateName,
                        Dob = item.candidatePersonalDetails.dob.ToString(),
                        Gender = item.candidatePersonalDetails.gender,
                        Religion = item.candidatePersonalDetails.religion,
                        Mobile = item.candidatePersonalDetails.mobile.ToString(),
                        Emailid = item.candidatePersonalDetails.emailId,
                        Ispwd = item.candidatePersonalDetails.isPWD.ToString(),
                        Pwddocumenturl = item.candidatePersonalDetails.pwdDocumentUrl,
                        Isminority = item.candidatePersonalDetails.isMinority.ToString(),
                        Minoritydocumenturl = item.candidatePersonalDetails.minorityDocumentUrl,
                        Isews = item.candidatePersonalDetails.isEWS.ToString(),
                        Ewsdocumenturl = item.candidatePersonalDetails.ewsDocumentUrl,
                        Aadharreference = item.candidatePersonalDetails.aadharReference,
                        Addressline1 = item.candidateAddress.addressLine1,
                        State = item.candidateAddress.state,
                        Stateid = item.candidateAddress.stateID,
                        District = item.candidateAddress.district,
                        Districtid = item.candidateAddress.districtId
                    };
                    if (item.candidateTrainingDetails != null)
                    {
                        foreach (var td in item.candidateTrainingDetails)
                        {
                            candidate.Candidatetrainingdetails.Add(new Candidatetrainingdetail
                            {
                                Candidateid = item.candidatePersonalDetails.candidateId,
                                Batchid = td.batchId,
                                Batchtype = td.batchtype,
                                Batchstage = td.batchStage,
                                Assessmentdate = td.AssessmentDate.ToString(),
                                Certificatelink = td.CertificateLink,
                                Subschemename = td.subSchemeName,
                                Tpid = td.tpId,
                                Tpname = td.tpName,
                                Tcid = td.tcId,
                                Tcname = td.tcName
                            });
                        }
                    }
                    _moprContext.Candidates.Add(candidate);
                }
                await _moprContext.SaveChangesAsync();
            }
            return Ok("Candidates saved successfully!");
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
