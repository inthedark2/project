using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class UserProfilesViewModel
    {
        [Required, Display(Name = "E-mail"), DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required, DataType(DataType.Password)]

        [Display(Name = "Full name")]
        public string FullName { get; set; }
        [Display(Name = "Address")]
        public string address { get; set; }
        [Display, DataType(DataType.PhoneNumber)]
        public string phone { get; set; }
    }
}