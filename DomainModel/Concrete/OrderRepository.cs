using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Concrete
{
    public class OrderRepository
    {
        EFContext context;
        private readonly PostRepository postRepository;
        public OrderRepository(EFContext context)
        {
            this.context = context;
            postRepository = new PostRepository(context);
        }

        public void NewOrder(User user)
        {
            Order order = new Order() { OrderStatus = GetStatusByName("New"), OrederTime = DateTime.Now, User = user };
            context.Orders.Add(order);
            context.SaveChanges();
            foreach (var p in postRepository.GetAllProductInBasket(user.email))
            {
                ProductInOrder product = new ProductInOrder() { ProductId = p.ProductId, Quantity = p.Quantity,OrderID=order.Id };
                context.ProductsInOrder.Add(product);
                context.SaveChanges();
                order.ProductInOrder.Add(product);
            }
            context.SaveChanges();
            ClearBusket(user);
        }
        public void ClearBusket(User user)
        {
            var products = postRepository.GetAllProductInBasket(user.email).ToList();
            foreach (var p in products)
            {
                context.ProductsInBasket.Remove(p);
                context.SaveChanges();
            }
        }

        public OrderStatus GetStatusByName(string name)
        {
            if(context.OrderStatus.Count()==0)
            {
                OrderStatus status = new OrderStatus { Name = "New" };
                OrderStatus status2 = new OrderStatus { Name = "In progress" };
                OrderStatus status3 = new OrderStatus { Name = "Complited" };
                OrderStatus status4 = new OrderStatus { Name = "Canceled" };
                context.OrderStatus.Add(status);
                context.OrderStatus.Add(status2);
                context.OrderStatus.Add(status3);
                context.OrderStatus.Add(status4);
                context.SaveChanges();
            }
            return context.OrderStatus.SingleOrDefault(s => s.Name == name);
        }
        public IQueryable<Order> GetOrder(User user)
        {
            return context.Orders.Where(r => r.User.id == user.id);
        }
    }
}
