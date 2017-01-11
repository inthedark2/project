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

namespace TeamProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserRepository userRepository;
        private readonly PostRepository postRepository;
        public HomeController()
        {
            EFContext context = new EFContext();
            userRepository = new UserRepository(context);
            postRepository = new PostRepository(context);
        }
        public ActionResult Index()
        {
            string path = "/MiniImages/";
            return View(from data in postRepository.GetAllProduct() select new IndexHomeViewModel {Id=data.Id,Title=data.Title,Price=data.Price,miniImage= path+data.Image.FirstOrDefault().MiniImage });
        }    
        public ActionResult Basket()
        {
            string path = "/MiniImages/";
            return View(from data in postRepository.GetAllProductInBasket(User.Identity.Name) select new BasketViewModel { ProductId = data.ProductId, ProductPrice = postRepository.GetProductById(data.ProductId).Price, Quantity = data.Quantity, MiniImage = path + postRepository.GetProductById(data.ProductId).Image.FirstOrDefault().MiniImage });
        }
        
        public ActionResult Registration()
        {
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
            Product product = postRepository.GetProductById(id);
            HomeProductViewModel model = new HomeProductViewModel() { Id = product.Id, Title = product.Title, Description = product.Description, Price = product.Price, images = product.Image,Quantity=product.Quantity };
            return View(model);
        }
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

    }
}