﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SinExWebApp20273938.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        //        [StringLength(10, MinimumLength = 6)]
        [Required]
        [Display(Name = "User name")]
        [StringLength(10)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User name")]
        [StringLength(10, MinimumLength = 6)]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "A user name should be between 6 and 10 characters and contain only letters and digits")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [StringLength(30)]
        public string Email { get; set; }
  
        [Required]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=\w*[^\w]\w*[^\w]\w*)(.){5,}$", ErrorMessage = "There are at least 2 nonalphanumeric characters in the passowrd")] //try -> OK!
        //[RegularExpression(@"^(?=[^a-zA-Z0-9]){2,}$", ErrorMessage = "There are at least 2 nonalphanumeric characters in the passowrd")] 
        //[RegularExpression(@"^(?=[^a-zA-Z0-9]){2,}$", ErrorMessage = "There are at least 2 nonalphanumeric characters in the passowrd")] 
        //[RegularExpression(@"^\w[^\\]*[^a-zA-Z\\][^\\]*", ErrorMessage = "There are at least 2 nonalphanumeric characters in the passowrd")] 
        //[RegularExpression(@"^[a-zA-Z]*[0-9\+\*][a-zA-Z0-9\+\*]*", ErrorMessage = "There are at least 2 nonalphanumeric characters in the passowrd")] 
        //[RegularExpression(@"[^a-zA-Z]", ErrorMessage = "There are at least 2 nonalphanumeric characters in the passowrd")] 
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
