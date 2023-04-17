﻿using CI_Platform.Entities.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entities.ViewModels
{
    public class AdminUserCreateVm
    {
        public long UserId { get; set; }


        [Required(ErrorMessage = "First name is required")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Only alphabets are allowed in first name")]
        public string? FirstName { get; set; }



        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Only alphabets are allowed in last name")]
        public string? LastName { get; set; }



        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; } = null!;



        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character")]
        public string Password { get; set; } = null!;



        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone number should be 10 digits long and contain numbers only")]
        public long PhoneNumber { get; set; }


        public string? Avatar { get; set; }

       

        public string? EmployeeId { get; set; }



        public string? Department { get; set; }



        [Required(ErrorMessage = "Please Select a City")]
        public long CityId { get; set; }



        [Required(ErrorMessage = "Please Select a Country")]
        public long CountryId { get; set; }



        public string? ProfileText { get; set; }



       [Required(ErrorMessage = "Please Select Status")]
        public int Status { get; set; }


        public List<Country> Countries { get; set; }
    }
}
