using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SocialMedia.BlazorServer.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("IdentityAPI");
        }

        public async Task<string> Login(string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("login", new { Email = email, Password = password });

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return result.Token;
            }

            return null;
        }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
    }
}

