using BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamProject.Controllers
{
    public class TestAutoFacController : Controller
    {
        private readonly IUserProvider _userProvider;
        public TestAutoFacController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }
        // GET: TestAutoFac
        public string Index()
        {
            var t = _userProvider.Login("sdfsf", "asdfsdf");
            return "View()";
        }
    }
}