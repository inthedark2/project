using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Concrete
{
    public class UserRepository
    {
        EFContext context;
        public UserRepository(EFContext context)
        {
            this.context = context;
        }
        public bool CreateUser(string email,string password,string name,string surname,string address,string phone)
        {
            User user = CreateUser(email, password);
            if (user!=null)
            {
                if (name!=null||surname!=null|address!=null||phone!=null)
                {
                    CreateProfile(user, name, surname, address, phone);
                }
                return true;
            }
            return false;
        }

        public User CreateUser(string email,string password)
        {
            User user = null;
            if (context.Users.Where(u=>u.email==email).FirstOrDefault()==null)
            {
                user = new User() { email = email, password = password, PasswordSalt = password };
                var crypto= new SimpleCrypto.PBKDF2();
                user.password = crypto.Compute(user.password);
                user.PasswordSalt = crypto.Salt;
                user.registeredDate = DateTime.Now;
                context.Users.Add(user);
                context.SaveChanges();
            }
            return user;
        }

        public void CreateProfile(User user,string name,string surname,string address,string phone)
        {
            UserProfile profile = new UserProfile() { Name = name, Surname = surname, Address = address, Phone = phone,User=user };
            context.UserProfiles.Add(profile);
            context.SaveChanges();
        }

    }
}
