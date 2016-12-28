using DomainModel.Concrete;
using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
    }
}