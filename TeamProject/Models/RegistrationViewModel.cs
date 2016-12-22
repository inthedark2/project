using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class RegistrationViewModel
    {
        [Required,Display(Name ="E-mail"),DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Confirm password"), Compare("Password", ErrorMessage = "Пароль має співпадати")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Name")]
        public string name { get; set; }
        [Display(Name = "Surname")]
        public string surname { get; set; }
        [Display(Name = "Address")]
        public string address { get; set; }
        [Display,DataType(DataType.PhoneNumber)]
        public string phone { get; set; }
    }
}