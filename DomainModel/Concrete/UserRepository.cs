using DomainModel.Abstract;
using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Concrete
{
    public class UserRepository: IUserRepository
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
                if (GetRoleByName("Client")!=null)
                {
                    user.Role = GetRoleByName("Client");
                }
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

        public bool FindUser(string email, string password)
        {
            User user= GetUserByEmail(email);
            if(user!=null)
            {
                var crypto = new SimpleCrypto.PBKDF2();
                if (user.password == crypto.Compute(password, user.PasswordSalt))
                    return true;
            }
            return false;
        }

        public IQueryable<User> Users()
        {
            return from data in context.Users orderby data.id descending select data;
        }

        public User GetUserByEmail(string email)
        {
            return this.Users().SingleOrDefault(u=>u.email==email);
        }
        public User GetUserById(int id)
        {
            return context.Users.SingleOrDefault(u => u.id == id);
        }
        public void EditUser(int id,string Email,int roleId)
        {
            User user = GetUserById(id);
            user.email = Email;
            user.Role = GetRoleById(roleId);
            context.SaveChanges();
        }

        public IQueryable<Role> GetAllRoles()
        {
            return from data in context.Roles orderby data.Id descending select data;
        }
        public bool AddRole(string name,string description)
        {
            Role role = new Role() { Name = name, Description = description };
            context.Roles.Add(role);
            context.SaveChanges();
            return true;
        }
        public Role GetRoleById(int id)
        {
            return context.Roles.SingleOrDefault(r => r.Id == id);
        }
        public void EditRole(int id,string name,string description)
        {
            Role role = GetRoleById(id);
            role.Name = name;
            role.Description = description;
            context.SaveChanges();
        }

        public void DeleteRole(Role role)
        {
            context.Roles.Remove(role);
            context.SaveChanges();
        }
        public Role GetRoleByName(string name)
        {
            return context.Roles.SingleOrDefault(r => r.Name == name);
        }

        public void EditProfile(int userId,string name,string surname,string address,string phone)
        {
            User user = GetUserById(userId);
            if (user.Profile==null)
            {
                CreateProfile(user, name, surname, address, phone);
            }
            else
            {
                user.Profile.Name = name;
                user.Profile.Surname = surname;
                user.Profile.Address = address;
                user.Profile.Phone = phone;
                context.SaveChanges();
            }
        }

        public IQueryable<User> GetAllUsers()
        {
            IQueryable<User> listUsers = from data in context.Set<User>().AsQueryable() select data;
            return listUsers;
        }
    }
}
