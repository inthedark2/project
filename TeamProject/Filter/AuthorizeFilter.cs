using DomainModel.Concrete;
using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace TeamProject.Filter
{
    public class AuthorizeFilterAttribute : AuthorizeAttribute
    {
        private string[] allowedUsers;
        private string allowedRoles;
        private readonly UserRepository userRepository;
        public AuthorizeFilterAttribute(string roles)
        {
            EFContext context = new EFContext();
            //allowedUsers = users;
            allowedRoles = roles;
            userRepository = new UserRepository(context);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //User user = userRepository.GetUserByEmail(httpContext.User.Identity.Name);
            return httpContext.Request.IsAuthenticated && allowedRoles.Contains(userRepository.GetUserByEmail(httpContext.User.Identity.Name).Role.Name);
        }
    }
}