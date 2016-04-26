using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNiDAYSHomework.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        public Guid UserID { get; set; }
        
        [Display(Name = "Email Address")]
        [Required]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 32 characters long.")]
        public string Password { get; set; }

        //used to store post-encrypted password
        public string EncryptedPassword { get; set; }
    }
}