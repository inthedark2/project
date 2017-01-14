using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class EFContext:DbContext
    {
        public EFContext():base("MyDbConnection")
        {
            Database.SetInitializer<EFContext>(null);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Basket> Basket { get; set; }
        public DbSet<ProductInBusket> ProductsInBasket { get; set; }
        public DbSet<ProductInOrder> ProductsInOrder { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
