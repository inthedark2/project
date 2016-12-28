using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class Images
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string MiniImage { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
