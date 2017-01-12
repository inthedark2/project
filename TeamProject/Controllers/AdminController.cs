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
using PagedList.Mvc;
using PagedList;
using TeamProject.Filter;

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
        [AuthorizeFilter("Admin")]
        public ActionResult Index()
        {
            return View();
        }

        //Products
        public ActionResult Products(int? page)
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            var items = from data in postRepository.GetAllProduct() select new ProductsViewModel { Id = data.Id, title = data.Title, category = data.category.Name, Quantity = data.Quantity, Time = data.AddTime, Price = data.Price };
            return View(items.ToPagedList(pageNumber,pageSize));
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
            if (postRepository.AddProduct(model.Title, model.Description, Image, model.Quantity, model.IsIn, model.categoryId,model.Price, path,miniPath))
            {
                return RedirectToAction("Products", "Admin");
            }
            else
                return View(model);
        }
        public ActionResult EditProduct(int id)
        {
            Product product = postRepository.GetProductById(id);
            if (product != null)
            {
                EditProductModel model = new EditProductModel() { Id = product.Id, Title = product.Title, Description = product.Description, IsIn = product.IsIn, Quantity = product.Quantity,Price=product.Price, categoryId = product.CategoryId };
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
            postRepository.EditProduct(model.Id, model.Title, model.Description, model.Quantity,model.IsIn, Image, model.categoryId,model.Price, path, miniPath);

            return RedirectToAction("Products", "admin");
        }
        [HttpPost]
        public JsonResult DeleteProduct(int id)
        {
            var productToDel = postRepository.GetProductById(id);
            if (productToDel != null)
            {
                string path = Server.MapPath(ConfigurationManager.AppSettings["imagesPath"]);
                string miniPath = Server.MapPath(ConfigurationManager.AppSettings["MiniImages"]);
                postRepository.RemoveProduct(productToDel, path, miniPath);
                return Json("Succsess");
            }
            return Json("Error");
        }

        public ActionResult DetailsProduct(int id)
        {
            var product = postRepository.GetProductById(id);
            ProductsViewModel model = new ProductsViewModel()
            {
                Id = product.Id,
                title = product.Title,
                category = product.category.Name,
                Quantity = product.Quantity,
                Time = product.AddTime,
                Photos = product.Image.Select(n => n.Name)
            };
            return View(model);
        }
        public ActionResult SearchProduct(string name)
        {
            var allproduct = postRepository.GetAllProduct().Where(a => a.Title.Contains(name)).ToList();
            return View(allproduct);
        }
        //Category
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
            if (delCat != null && delCat.Posts.Count == 0)
            {
                categoryRepository.RemoveCategory(delCat);
                return Json("Succsess");
            }
            return Json("Error");
        }

        //Users
        public ActionResult Users()
        {
            return View(from data in userRepository.Users() select new UsersViewModel() { Id = data.id, Email = data.email, time = data.registeredDate });
        }
        public ActionResult EditUser(int id)
        {
            User user = userRepository.GetUserById(id);
            if (user!=null)
            {
                EditUserModel model = new EditUserModel() { Id = user.id, Email = user.email,RoleId=user.Role.Id };
                ViewBag.ListCategory = userRepository.GetAllRoles().ToList();
                return View(model);
            }
            ModelState.AddModelError("", "Сталась помилка");
            return View();
        }
        [HttpPost]
        public ActionResult EditUser(EditUserModel model)
        {
            if (userRepository.GetUserById(model.Id)!=null)
            {
                userRepository.EditUser(model.Id, model.Email, model.RoleId);
                return RedirectToAction("Users", "Admin");
            }
            ModelState.AddModelError("", "Сталась помилка");
            return View();
        }
        //Roles
        public ActionResult Roles()
        {
            return View(from data in userRepository.GetAllRoles() select new RolesViewModel() { Id=data.Id,Name=data.Name,Description=data.Description,QuantityUsers=data.Users.Count});
        }
        public ActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddRole(AddRoleModel model)
        {
            if (userRepository.AddRole(model.Name,model.Description))
            {
                return RedirectToAction("Roles");
            }
            else
            {
                ModelState.AddModelError("", "Сталась помилка");
                return View();
            }
        } 
        public ActionResult EditRole(int id)
        {
            Role role = userRepository.GetRoleById(id);
            if (role!=null)
            {
                RolesViewModel model = new RolesViewModel() { Id = role.Id, Name = role.Name, Description = role.Description };
                return View(model);
            }
            ModelState.AddModelError("", "Сталась помилка");
            return View();
        }
        [HttpPost]
        public ActionResult EditRole(RolesViewModel model)
        {
            if (userRepository.GetRoleById(model.Id) != null)
            {
                userRepository.EditRole(model.Id, model.Name, model.Description);
                return RedirectToAction("Roles", "Admin");
            }
            ModelState.AddModelError("", "Сталась помилка");
            return View();
        }
        [HttpPost]
        public JsonResult DeleteRole(int id)
        {
            var delRole = userRepository.GetRoleById(id);
            if (delRole != null && delRole.Users.Count == 0)
            {
                userRepository.DeleteRole(delRole);
                return Json("Succsess");
            }
            return Json("Error");
        }
    }
}