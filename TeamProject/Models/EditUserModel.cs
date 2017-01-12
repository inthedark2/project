using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class EditUserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}