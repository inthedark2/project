using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class RolesViewModel
    {
        [Required, Display(Name = "ID")]
        public int Id { get; set; }
        [Required,Display(Name ="Name")]
        public string Name { get; set; }
        [Required, Display(Name = "Description")]
        public string Description { get; set; }
        [Required, Display(Name = "QuantityUsers")]
        public int QuantityUsers { get; set; }
    }
}