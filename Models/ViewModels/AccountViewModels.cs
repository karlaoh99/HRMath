using System.ComponentModel.DataAnnotations;

namespace HRMath.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage="Enter your email")]
        public string Email {get; set;}

        [Required(ErrorMessage="Enter your password")]
        public string Password {get; set;}
    }

    public class SignupModel
    {
        [Required(ErrorMessage="Enter a user name")]
        public string Name {get; set;}
        
        [Required(ErrorMessage="Enter an email")]
        //[EmailAddress(ErrorMessage="Enter a valid email address")]
        public string Email {get; set;}

        [Required(ErrorMessage="Enter a password")]
        public string Password {get; set;}

    }



}