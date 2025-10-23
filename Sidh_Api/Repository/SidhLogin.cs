using Newtonsoft.Json;
using System.Net;
using Sidh_Api.Database;


public class SidhLogin
{
    public class CsrfResponse
    {
        public string csrfToken { get; set; }
        public string Token { get; set; }
    }

    public class loginResponse
    {
        public string loginToken { get; set; }
    }

    public async Task<string> GetTokenAsync()
    {
        try
        {
            var handler = new HttpClientHandler
            {
                CookieContainer = new CookieContainer()
            };
            using var client = new HttpClient(handler);
            var csrfResponse = await client.GetAsync("https://localhost:7056/api/CsrfTokenGeneration/CheckUserHeaders");
            csrfResponse.EnsureSuccessStatusCode();
            var csrfJson = await csrfResponse.Content.ReadAsStringAsync();
            var csrfObj = JsonConvert.DeserializeObject<CsrfResponse>(csrfJson);
            return csrfObj.csrfToken;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("Error calling the API", ex);
        }
    }
}
