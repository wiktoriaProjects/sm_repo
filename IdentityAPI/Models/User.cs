using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace IdentityApi.Models
{
   [Table("user", Schema = "dbo")]
    public class User : IdentityUser
    {
       // public string? FirstName {  get; set; }
    }
}

//namespace IdentityApi.Models
//{
//    [Table("users", Schema = "dbo")]
//    public class User
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

//        [Column("user_id")]
//        public int UserId { get; set; }

//        [Required]
//        [Column("username")]
//        public string Username { get; set; }

//        [Required]
//        [Column("email")]
//        public string Email { get; set; }

//        [Required]
//        [Column("password")]
//        public string Password { get; set; }

//        //[Column("created_at")]
//        //public DateTime CreatedAt { get; set; } = DateTime.Now;


//        [Column("image")]
//        public string? ProfileImageUrl { get; set; }
//    }
//}

