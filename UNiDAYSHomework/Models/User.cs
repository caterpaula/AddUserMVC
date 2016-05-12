using FluentValidation.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace UNiDAYSHomework.Models
{
    [Validator(typeof(UserValidator))]
    public class User
    {
        public Guid UserID { get; set; }
        
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        public string Password { get; set; }

        //used to store post-encrypted password
        public string EncryptedPassword { get; set; }
    }
}