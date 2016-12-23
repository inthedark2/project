using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class AddCategoryModel
    {
        [Required, DataType(DataType.Text),Display(Name ="Name")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText),Display(Name ="Description")]
        public string Description { get; set; }

    }
}