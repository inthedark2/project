using DomainModel.Concrete;
using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Models;

namespace TeamProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly PostRepository postRepository;
        public readonly CategoryRepository categoryRepository;
        public AdminController()
        {
            EFContext context = new EFContext();
            postRepository = new PostRepository(context);
            categoryRepository = new CategoryRepository(context);

        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Products()
        {
            return View(from data in postRepository.GetAllProduct() select new ProductsViewModel { Id=data.Id,title=data.Title,category=data.category.Name,Quantity=data.Quantity,Time=data.AddTime.Date});
        }
        public ActionResult Category()
        {
            return View(from data in categoryRepository.GetAllCategory() select new CategoryViewModel { Id = data.Id, Name = data.Name, Quantity = data.Posts.Count });
        }
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(AddCategoryModel model)
        {
            if (categoryRepository.AddCategory(model.Name,model.Description))
            {
                return RedirectToAction("category", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "Сталась помилка");
                return View();
            }
        }
    }
}