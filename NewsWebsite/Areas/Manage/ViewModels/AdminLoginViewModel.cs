﻿using System.ComponentModel.DataAnnotations;

namespace NewsWebsite.Areas.Manage.ViewModels
{
    public class AdminLoginViewModel
    {
        [MaxLength(20)]
        public string Username { get; set;}
        [MaxLength(20)]
        public string Password { get; set;}
        public bool IsPersist { get; set; }
    }
}
