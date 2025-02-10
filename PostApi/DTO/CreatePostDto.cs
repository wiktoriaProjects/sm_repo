using System.ComponentModel.DataAnnotations;

namespace PostApi.DTO
{
    public class CreatePostDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
