using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Abstract
{
    public interface IUserRepository
    {
        User FindUser(string email, string password);
        IQueryable<User> Users();
        User GetUserByEmail(string email);
    }
}
