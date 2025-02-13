using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeopleApi.Models
{
    public class Friend { 
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }  // sent the request

        [Required]
        public string FriendId { get; set; } // receiving the request

        [Required]
        public FriendStatus Status { get; set; } = FriendStatus.Pending;

        // Relation
        [ForeignKey("UserId")]
        public UserProfile User { get; set; }

        [ForeignKey("FriendId")]
        public UserProfile FriendProfile { get; set; }
    }

    public enum FriendStatus
    {
        Pending,
        Accepted,
        Rejected
    }
}

