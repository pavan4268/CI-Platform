﻿using CI_Platform.Entities.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entities.ViewModels
{
    public class ResetVm
    {
        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Confirm_Password { get; set; } = null!;

        public string Email { get; set; }= null!;

        public string Token { get; set; } = null!;  

        public List<Banner>? banners { get; set; }
    }
}
