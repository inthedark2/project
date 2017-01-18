using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class UserViewModel
    {
        [Required, Display(Name = "ID")]
        public int Id { get; set; }
        [Required, Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Register date")]
        public DateTime time;
    }
}
