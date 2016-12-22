using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class User
    {
        [Key]
        public int id { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required,DataType(DataType.Password)]
        public string password { get; set; }
        [Required, StringLength(maximumLength: 200)]
        public string PasswordSalt { get; set; }
        [Required]
        public DateTime registeredDate { get; set; }
        public virtual Role Role { get; set; }
        public virtual UserProfile Profile { get; set; }
        public virtual Basket Basket { get; set; }

    }
}
