using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(maximumLength: 100)]
        public string Title { get; set; }
        [Required, StringLength(maximumLength: 1500)]
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        public bool IsIn { get; set; }
        public DateTime AddTime { get; set; }
        public ICollection<Images> Image { get; set; }



    }
}
