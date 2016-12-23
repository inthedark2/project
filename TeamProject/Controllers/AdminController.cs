using DomainModel.Concrete;
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
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Products()
        {
            return View(from data in postRepository.GetAllProduct() select new ProductsViewModel { Id=data.Id,title=data.Title,category=data.category.Name,Quantity=data.Quantity,Time=data.AddTime.Date});
        }
    }
}