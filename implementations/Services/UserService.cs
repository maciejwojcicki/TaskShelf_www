using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using implementations.Exceptions;
using System.Security.Principal;
using implementations.Models;
using database;
using database.Entities;
using implementations.Interfaces;
using implementations.Utils;

namespace implementations.Services
{
    public class UserService : IUserService
    {
        private Model1 context;
        public UserService()
        {

            this.context = new Model1();
            this.context.Configuration.LazyLoadingEnabled = false;
            this.context.Database.Log = (text) =>
            {
                Console.WriteLine(text);
            };
        }
        public int Login(LoginModel model)
        {
            ModelUtils.Validate(model);

            var hash = UserUtils.PasswordHash(model.Password);

            var user = context.Set<User>()
                .Where(w => w.Login == model.Login && w.Password == hash)
                //.Include(i => i.Permissions)               
                .SingleOrDefault();

            if (user == null)
            {
                throw new InvalidLoginPasswordException();
            }


            //if (user.ActivationToken != null)
            //{
            //    throw new AccountNotActivatedException();
            //}

            //if (user.Permissions.Select(p => p.Value)
            //    .Contains(Permission.PermissionList.CanLogin) == false)
            //{
            //    throw new PermissionException(Permission.PermissionList.CanLogin);
            //}

            int userId = user.UserId;

            return userId;
        }

        public IPrincipal GetPrincipal(int userId)
        {
            var user = context.Set<User>()
                //.AsQueryable()
                .Where(w => w.UserId == userId)
                //.Include(i => i.Permissions)
                .SingleOrDefault();

            if (user == null)
            {
                throw new NotFoundException();
            }

            string[] roles = context.Set<Permission>().Select(p => p.Name).ToArray();
            //string[] roles = { "test" };
            return new GenericPrincipal(
                new GenericIdentity(user.UserId.ToString()),
                roles);
        }

        public User GetCurrentUser(IPrincipal currentPrincipal)
        {
            User user = null;
            int userId = 0;
            if (currentPrincipal != null && currentPrincipal.Identity != null && int.TryParse(currentPrincipal.Identity.Name, out userId))
            {
                user = context.Set<User>().Where(i => i.UserId == userId).Single();
            }
            return user;
        }
        public User GetUserById(int userId)
        {
            var user = context.Set<User>().Single(p => p.UserId == userId);
            return user;
        }

        public void Register(RegisterModel model)
        {
            ModelUtils.Validate(model);

            if (context.Set<User>().Where(w => w.Login.ToLower() == model.Login.ToLower()).Count() > 0)
            {
                throw new LoginInUseException();
            }


            var user = new User();
            user.Login = model.Login;
            user.Name = model.Name;
            user.Password = UserUtils.PasswordHash(model.Password);
            user.Email = model.Email;
            user.ActivationToken = model.ActivationToken;
            context.Set<User>().Add(user);
            context.SaveChanges();

            //user.Permissions = new List<Permission>
            //{
            //    context.Set<Permission>()
            //    .Single(p=>p.Name == "CanLogin")

            //};

        }
    }
}
