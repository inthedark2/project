using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class EditProductModel
    {
        public int Id { get; set; }
        [Required, DataType(DataType.Text), Display(Name = "Title")]
        public string Title { get; set; }
        [Required, DataType(DataType.MultilineText), Display(Name = "Description")]
        public string Description { get; set; }
        [Required, Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Required, Display(Name = "В наявності")]
        public bool IsIn { get; set; }
        [Required, Display(Name = "Select category")]
        public int categoryId { get; set; }
    }
}