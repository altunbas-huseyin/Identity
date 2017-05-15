﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IdentityModels.Users
{
    public class UserRegisterView
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set; }
        [Required]
        public string FirmCode { get; set; }
        public string FirmLogo { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string FirmName { get; set; }
        public dynamic Extra1 { get; set; }
        public dynamic Extra2 { get; set; }
    }
}