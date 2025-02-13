using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace sm_Blazor.Services
{
    public class PostService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;

        public PostService(HttpClient httpClient, AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<List<PostDto>> GetPosts()
        {
            return await _httpClient.GetFromJsonAsync<List<PostDto>>("https://localhost:7187/api/posts");
            //string userId = await _authService.GetUserId();
            //if (string.IsNullOrEmpty(userId)) return new List<PostDto>();
            //return await _httpClient.GetFromJsonAsync<List<PostDto>>($"api/posts/user/{userId}");
        }

        public async Task<bool> CreatePost(CreatePostDto postDto)
        {
            //var response = await _httpClient.PostAsJsonAsync("https://localhost:7187/api/posts", postDto);


            //string userId = await _authService.GetUserId();
            //if (string.IsNullOrEmpty(userId)) return false;

            //postDto.UserId = userId;

            var userId = await _authService.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                Console.WriteLine("No user ID found! You must be logged in.");
                return false;
            }
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7187/api/posts", postDto);

            return response.IsSuccessStatusCode;
        }
    }

    public class PostDto
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CreatedAt { get; set; }
        public string UserName { get; set; }
    }

    
    public class CreatePostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

    }
}
