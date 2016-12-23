using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Concrete
{
    public class CategoryRepository
    {
        EFContext context;
        public CategoryRepository(EFContext context)
        {
            this.context = context;
        }
        public IQueryable<Category> GetAllCategory()
        {
            return from data in context.Category select data;
        }
        public bool AddCategory(string name,string description)
        {
            Category category = null;
            if (context.Category.Where(c=>c.Name==name).FirstOrDefault()==null)
            {
                category = new Category() { Name = name, Description = description };
                context.Category.Add(category);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public Category GetCategoryById(int id)
        {
            return context.Category.Where(c=>c.Id==id).FirstOrDefault();
        }
        public void EditCategory(int id,string name,string description)
        {
            Category category = context.Category.Where(c => c.Id == id).First();
            category.Name = name;
            category.Description = description;
            context.SaveChanges();
        }
    }
}
