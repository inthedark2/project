using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class Basket
    {
        [ForeignKey("User")]
        public int Id { get; set; }
        public virtual User User { get; set; }
        public ICollection<ProductInBusket> Products { get; set; }
    }
}
