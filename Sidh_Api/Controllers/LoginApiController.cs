using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


[ApiController]
[Route("api/[controller]")]

public class LoginApiController : ControllerBase

{
    private readonly HttpClient _httpClient;
    private readonly SidhLogin _sidhLogin;


    public LoginApiController(HttpClient httpClient, SidhLogin sidhLogin)
    {
        _httpClient = httpClient;
        _sidhLogin = sidhLogin;
    }

    [HttpPost("Login")]
    public async Task<string> LoginAsync()
    {
        var url = "https://uat.ekaushal.com/api/user/v1/login";
        var payload = new
        {
            userName = "ExternalService_03_RDS",
            password = "RBrl6uYjL5ZY0aLDMvIKnz0Wf8apt/5LBQwq35UmQaKEbboH6abgNJWUD5W9oEMojlZZEP7f5sk+i7NA0vpCGZLs/uIkpBVLBQvMQDBVoiSYth875/BZVzLFxbxrwz+bd2s34GiHw49b3NihxCUImvqJL/CyRYWaOiWIgBWIjuQ=tDyt2"
        };

        var json = JsonSerializer.Serialize(payload);
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
