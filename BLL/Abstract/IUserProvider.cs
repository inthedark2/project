using BLL.Helpers;
using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IUserProvider
    {
        UserStatus Login(string login, string password);

        IQueryable<UserViewModel> GetListUsers();
    }
}
