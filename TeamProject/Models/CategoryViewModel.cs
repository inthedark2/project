using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class CategoryViewModel
    {
        [Required, Display(Name = "ID")]
        public int Id { get; set; }
        [Required, Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
    }
}