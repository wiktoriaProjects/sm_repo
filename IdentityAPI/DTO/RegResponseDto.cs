namespace IdentityApi.DTO
{
    public class RegResponseDto
    {
        public bool IsSuccessfulRegistration {  get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
