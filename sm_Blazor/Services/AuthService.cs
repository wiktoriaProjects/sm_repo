
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorWasmHttp.Services;
using Microsoft.JSInterop;

namespace sm_Blazor.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly CustomAuthStateProvider _authStateProvider;

        public AuthService(HttpClient httpClient, IJSRuntime jsRuntime, CustomAuthStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _authStateProvider = authStateProvider;
    }

        // Register User
        public async Task<bool> Register(string userName, string email, string password, string confirmPassword)
        {
            var response = await _httpClient.PostAsJsonAsync("register", new
            {
                UserName = userName,
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            });

            return response.IsSuccessStatusCode;
        }


        public async Task<bool> Login(string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("login", new { Email = email, Password = password });

            //1
            //if (response.IsSuccessStatusCode)
            //{
            //    var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            //    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", result.Token);
            //    return true;
            //}
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (result != null && !string.IsNullOrEmpty(result.Token))
                {
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", result.Token);
                    await _authStateProvider.MarkUserAsAuthenticated(result.Token);
                    return true;
                }
            }

            return false;
        }

        public async Task<string> GetToken()
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken"); //  
           
        }

        //  LogouRemove Token
        public async Task Logout()
        {
           
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
            await _authStateProvider.MarkUserAsLoggedOut();//
        }

        // Use id from token
        public async Task<string> GetUserId()
        {
            return _authStateProvider.GetUserId();
        }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
    }
}













//using System.Net.Http.Json;
//using System.Threading.Tasks;

//namespace sm_Blazor.Services
//{
//    public class AuthService
//    {
//        private readonly HttpClient _httpClient;

//        public AuthService(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//        }

//        public async Task<string> Login(string email, string password)
//        {
//            var response = await _httpClient.PostAsJsonAsync("login", new { Email = email, Password = password });

//            if (response.IsSuccessStatusCode)
//            {
//                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
//                return result.Token;
//            }

//            return null;
//        }
//    }

//    public class LoginResponse
//    {
//        public string Token { get; set; }
//    }
//}
