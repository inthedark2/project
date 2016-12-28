using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class UsersViewModel
    {
        [Required, Display(Name = "ID")]
        public int Id { get; set; }
        [Required,Display(Name ="Email")]
        public string Email { get; set; }
        [Display(Name ="Register date")]
        public DateTime time;
    }
}