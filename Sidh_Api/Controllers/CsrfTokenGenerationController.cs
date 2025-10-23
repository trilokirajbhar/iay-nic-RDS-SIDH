using Microsoft.AspNetCore.Mvc;


namespace Sidh_Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CsrfTokenGenerationController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CsrfTokenGenerationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("CheckUserHeaders")]
        public async Task<IActionResult> CheckUserHeaders()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var request = new HttpRequestMessage(HttpMethod.Head, "https://uat.ekaushal.com/api/user/v1");
                var response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, "API call failed");
                if (response.Headers.TryGetValues("X-Csrf-Token", out var csrfValues))
                {
                    string csrfToken = csrfValues.FirstOrDefault();
                    return Ok(new { csrfToken });
                }
                return NotFound("X-Csrf-Token header not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }

}
