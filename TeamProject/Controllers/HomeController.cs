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
    public class HomeController : Controller
    {
        private readonly UserRepository userRepository;
        public HomeController()
        {
            EFContext context = new EFContext();
            userRepository = new UserRepository(context);
        }
        public ActionResult Index()
        {
            return View();
        }    
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(RegistrationViewModel model)
        {
            if (userRepository.CreateUser(model.email,model.Password,model.name,model.surname,model.address,model.phone))
            {
                return RedirectToAction("index", "home");
            }
            else
            {
                ModelState.AddModelError("", "Error");
                return View();
            }
        }
    }
}