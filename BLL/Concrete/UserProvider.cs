using BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Helpers;
using DomainModel;
using DomainModel.Abstract;

namespace BLL.Concrete
{
    public class UserProvider : IUserProvider
    {
        private readonly IUserRepository _userRepository;
        public UserProvider(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public UserStatus Login(string login, string password)
        {
            var user = _userRepository.FindUser(login, password);
            if (user == null)
                return UserStatus.NotFoundUser;
            return UserStatus.Success;
        }
    }
}
