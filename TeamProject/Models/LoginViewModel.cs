using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TeamProject.Models
{
    public class LoginViewModel
    {

       
            [Required, Display(Name = "E-mail"), DataType(DataType.EmailAddress)]
            public string email { get; set; }
            [Required, DataType(DataType.Password)]
            public string Password { get; set; }

            
        }



    }
