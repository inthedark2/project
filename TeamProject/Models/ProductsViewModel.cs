using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class ProductsViewModel
    {
        [Required, Display(Name = "ID")]
        public int Id { get; set; }
        [Required, Display(Name = "Title")]
        public string title { get; set; }
        [Required, Display(Name = "Category")]
        public string category { get; set; }
        [Display(Name = "Quantity")]
        public int Quantity {get;set;}
        [Display(Name = "Time")]
        public DateTime Time { get; set; }
    }
}