using DomainModel.Concrete;
using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TeamProject.Models;
using PagedList.Mvc;
using PagedList;
namespace TeamProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserRepository userRepository;
        private readonly PostRepository postRepository;
        private readonly CategoryRepository categoryRepository;
        public HomeController()
        {
            EFContext context = new EFContext();
            userRepository = new UserRepository(context);
            postRepository = new PostRepository(context);
            categoryRepository = new CategoryRepository(context);
        }
        public ActionResult Index(int? page)
        {
            string path = "/MiniImages/";
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.TotalPrice = postRepository.TotalBasketPrice(userRepository.GetUserByEmail(User.Identity.Name));
            ViewBag.CountProductInBasket = postRepository.GetCountOfBasket(userRepository.GetUserByEmail(User.Identity.Name));
            ViewBag.ListCategory = categoryRepository.GetAllCategory().ToList();
            var posts = from data in postRepository.GetAllProduct() select new IndexHomeViewModel { Id = data.Id, Title = data.Title, Price = data.Price, miniImage = path + data.Image.FirstOrDefault().MiniImage,Description=data.Description };
            return View(posts.ToPagedList(pageNumber, pageSize));
        }    
        public ActionResult Basket()
        {
            string path = "/MiniImages/";
            ViewBag.TotalPrice = postRepository.TotalBasketPrice(userRepository.GetUserByEmail(User.Identity.Name));
            ViewBag.CountProductInBasket = postRepository.GetCountOfBasket(userRepository.GetUserByEmail(User.Identity.Name));
            ViewBag.ListCategory = categoryRepository.GetAllCategory().ToList();
            return View(from data in postRepository.GetAllProductInBasket(User.Identity.Name) select new BasketViewModel { ProductId = data.ProductId, ProductPrice = postRepository.GetProductById(data.ProductId).Price, Quantity = data.Quantity, MiniImage = path + postRepository.GetProductById(data.ProductId).Image.FirstOrDefault().MiniImage });
        }
       
        public ActionResult Registration()
        {
            ViewBag.TotalPrice = postRepository.TotalBasketPrice(userRepository.GetUserByEmail(User.Identity.Name));
            ViewBag.CountProductInBasket = postRepository.GetCountOfBasket(userRepository.GetUserByEmail(User.Identity.Name));
            ViewBag.ListCategory = categoryRepository.GetAllCategory().ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Registration(RegistrationViewModel model)
        {
            if (userRepository.CreateUser(model.email, model.Password, model.name, model.surname, model.address, model.phone))
            {
                return RedirectToAction("index", "home");
            }
            else
            {
                ModelState.AddModelError("", "Error");
                return View();
            }
        }

        public ActionResult Login()
        {
            ViewBag.TotalPrice = postRepository.TotalBasketPrice(userRepository.GetUserByEmail(User.Identity.Name));
            ViewBag.CountProductInBasket = postRepository.GetCountOfBasket(userRepository.GetUserByEmail(User.Identity.Name));
            ViewBag.ListCategory = categoryRepository.GetAllCategory().ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (userRepository.FindUser(model.email,model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.email, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login and password incorrect.");
                }
            }
            return View(model);
        }
        public ActionResult Details(int id)
        {
            ViewBag.TotalPrice = postRepository.TotalBasketPrice(userRepository.GetUserByEmail(User.Identity.Name));
            ViewBag.CountProductInBasket = postRepository.GetCountOfBasket(userRepository.GetUserByEmail(User.Identity.Name));
            ViewBag.ListCategory = categoryRepository.GetAllCategory().ToList();
            Product product = postRepository.GetProductById(id);
            HomeProductViewModel model = new HomeProductViewModel() { Id = product.Id, Title = product.Title, Description = product.Description, Price = product.Price, images = product.Image,Quantity=product.Quantity };
            return View(model);
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public JsonResult AddToCart(int productId,int quantity)
        {

            string email = User.Identity.Name;
            postRepository.AddProductToCart(productId, quantity, userRepository.GetUserByEmail(email));
            return Json("Succsess");
        }
        [Authorize]
        [HttpPost]
        public JsonResult DeleteProduct(int id)
        {
            string email = User.Identity.Name;
            postRepository.DeleteProductFromBasket(id, userRepository.GetUserByEmail(email));
            return Json("Succsess");
        }

        public ActionResult Category(int id)
        {
            ViewBag.TotalPrice = postRepository.TotalBasketPrice(userRepository.GetUserByEmail(User.Identity.Name));
            ViewBag.CountProductInBasket = postRepository.GetCountOfBasket(userRepository.GetUserByEmail(User.Identity.Name));
            ViewBag.ListCategory = categoryRepository.GetAllCategory().ToList();
            string path = "/MiniImages/";
            return View(from data in postRepository.GetProductsInCategory(id) select new IndexHomeViewModel { Id = data.Id, Title = data.Title, Price = data.Price, miniImage = path + data.Image.FirstOrDefault().MiniImage,Description=data.Description });
        }

        [Authorize]
        public ActionResult Profile()
        {
            ViewBag.TotalPrice = postRepository.TotalBasketPrice(userRepository.GetUserByEmail(User.Identity.Name));
            ViewBag.CountProductInBasket = postRepository.GetCountOfBasket(userRepository.GetUserByEmail(User.Identity.Name));
            ViewBag.ListCategory = categoryRepository.GetAllCategory().ToList();
            User user = userRepository.GetUserByEmail(User.Identity.Name);
            ProfileViewModel model = new ProfileViewModel() { Id=user.id,Email=user.email};
            if (user.Profile!=null)
            {
                model.Address = user.Profile.Address;
                model.Phone = user.Profile.Phone;
                model.FullName = user.Profile.Name + user.Profile.Surname;
            }
            return View(model);
        }
        [Authorize]
        public ActionResult EditProfile()
        {
            ViewBag.TotalPrice = postRepository.TotalBasketPrice(userRepository.GetUserByEmail(User.Identity.Name));
            ViewBag.CountProductInBasket = postRepository.GetCountOfBasket(userRepository.GetUserByEmail(User.Identity.Name));
            ViewBag.ListCategory = categoryRepository.GetAllCategory().ToList();
            User user = userRepository.GetUserByEmail(User.Identity.Name);
            EditProfileModel model = new EditProfileModel() { Id=user.id};
            if (user.Profile!=null)
            {
                model.Name = user.Profile.Name;
                model.Surname = user.Profile.Surname;
                model.Address = user.Profile.Address;
                model.Phone = user.Profile.Phone;
            }
            return View(model);   
        }
        [Authorize]
        [HttpPost]
        public ActionResult EditProfile(EditProfileModel model)
        {
            userRepository.EditProfile(model.Id, model.Name, model.Surname, model.Address, model.Phone);
            return RedirectToAction("Profile");
        }
    }
}