﻿using System.ComponentModel.DataAnnotations;

namespace Netolex.ViewModels.Account
{
    public class SignInVM
    {
        [Required(ErrorMessage = "Email is required")]
        //[EmailAddress(ErrorMessage = "Invalid email format")]
        public string EmailOrUsername { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        //[StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
