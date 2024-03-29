﻿using System.ComponentModel.DataAnnotations;

namespace RegService.ViewModel
{
    public class LoginViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? ContactNo { get; set; }
        public string? ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}
