using BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamProject.Controllers
{
    public class UsersController : Controller
    {
        // private Context context;
        private readonly IUserProvider _userProvider;

        public UsersController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

    }
}