namespace IdentityApi.DTO
{
    public class LoginResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string Token { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
