using DomainModel;
using DomainModel.Concrete;
using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
        private readonly UserRepository userRepository;
        public AdminController()
        {
            EFContext context = new EFContext();
            postRepository = new PostRepository(context);
            categoryRepository = new CategoryRepository(context);
            userRepository = new UserRepository(context);

        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Products()
        {
            return View(from data in postRepository.GetAllProduct() select new ProductsViewModel { Id = data.Id, title = data.Title, category = data.category.Name, Quantity = data.Quantity, Time = data.AddTime });
        }
        public ActionResult AddProduct()
        {
            AddProductModel model = new AddProductModel();
            ViewBag.ListCategory = categoryRepository.GetAllCategory().ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddProduct(AddProductModel model, HttpPostedFileBase[] Image)
        {
            string path = Server.MapPath(ConfigurationManager.AppSettings["imagesPath"]);
            string miniPath = Server.MapPath(ConfigurationManager.AppSettings["MiniImages"]);
            if (postRepository.AddProduct(model.Title, model.Description, Image, model.Quantity, model.IsIn, model.categoryId, path,miniPath))
            {
                return RedirectToAction("Products", "Admin");
            }
            else
                return View(model);
        }
        public ActionResult EditProduct(int id)
        {
            Product product = postRepository.GetProductById(id);
            if (product!=null)
            {
                EditProductModel model = new EditProductModel() { Id = product.Id, Title = product.Title, Description = product.Description, IsIn = product.IsIn, Quantity = product.Quantity, categoryId = product.CategoryId };
                ViewBag.ListCategory = categoryRepository.GetAllCategory().ToList();
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public ActionResult EditProduct(EditProductModel model, HttpPostedFileBase[] Image)
        {
            string path = Server.MapPath(ConfigurationManager.AppSettings["imagesPath"]);
            string miniPath = Server.MapPath(ConfigurationManager.AppSettings["MiniImages"]);
            postRepository.DeleteImages(model.Id, path, miniPath);
            postRepository.EditProduct(model.Id, model.Title, model.Description, model.Quantity,model.IsIn, Image, model.categoryId, path, miniPath);
            return RedirectToAction("Products", "admin");
        }
        [HttpPost]
        public JsonResult DeleteProduct(int id)
        {
            var productToDel = postRepository.GetProductById(id);
            if (productToDel!=null)
            {
                string path = Server.MapPath(ConfigurationManager.AppSettings["imagesPath"]);
                string miniPath = Server.MapPath(ConfigurationManager.AppSettings["MiniImages"]);
                postRepository.RemoveProduct(productToDel, path, miniPath);
                return Json("Succsess");
            }
            return Json("Error");
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
            if (categoryRepository.AddCategory(model.Name, model.Description))
            {
                return RedirectToAction("category", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "Сталась помилка");
                return View();
            }
        }
        public ActionResult EditCategory(int id)
        {
            Category category = categoryRepository.GetCategoryById(id);
            if (category != null)
            {
                EditCtegoryModel model = new EditCtegoryModel() { Id = category.Id, Name = category.Name, Description = category.Description };
                return View(model);
            }
            ModelState.AddModelError("", "Сталась помилка");
            return View();
        }
        [HttpPost]
        public ActionResult EditCategory(EditCtegoryModel model)
        {
            if (categoryRepository.GetCategoryById(model.Id) != null)
            {
                categoryRepository.EditCategory(model.Id, model.Name, model.Description);
                return RedirectToAction("Category", "Admin");
            }
            ModelState.AddModelError("", "Сталась помилка");
            return View();
        }
        [HttpPost]
        public JsonResult DeleteCategory(int id)
        {
            var delCat = categoryRepository.GetCategoryById(id);
            if (delCat!=null&&delCat.Posts.Count==0)
            {
                categoryRepository.RemoveCategory(delCat);
                return Json("Succsess");
            }
            return Json("Error");
        }
        public ActionResult Users()
        {
            return View(from data in userRepository.Users() select new UsersViewModel() { Id=data.id,Email=data.email,time=data.registeredDate});
        }
    }
}