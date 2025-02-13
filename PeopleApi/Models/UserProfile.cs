using System.ComponentModel.DataAnnotations;

namespace PeopleApi.Models
{
    public class UserProfile
    {
        [Key]
        public string Id { get; set; }  // =Identity API User ID
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicture { get; set; }
        public string Bio { get; set; }
        public List<Friend> Friends { get; set; } = new List<Friend>();
    }
}
