using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class BasketViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string MiniImage { get; set; }
        public double ProductPrice { get; set; }
    }
}