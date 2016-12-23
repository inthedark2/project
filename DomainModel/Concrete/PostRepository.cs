using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Concrete
{
    public class PostRepository
    {
        EFContext context;
        public PostRepository(EFContext context)
        {
            this.context = context;
        }
        public IQueryable<Product> GetAllProduct()
        {
            return from data in context.Products orderby data.Id descending select data;
        }
    }
}
