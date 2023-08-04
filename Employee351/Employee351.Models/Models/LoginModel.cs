﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee351.Models.Models
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set;}
        [Required]
        public string Password { get; set;}
    }

    public class LoginResponseModel
    {
        public int Id { get; set;}
        public string Email { get; set;}
        public string JWT_Token { get; set;}
        public string OTP { get; set;}
    }
}
