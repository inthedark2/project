using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class HomeProductViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public ICollection<Images> images { get; set; }
        public int Quantity { get; set; }


    }
}