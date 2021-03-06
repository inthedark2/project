﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
    }
}