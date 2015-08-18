using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using database;
using implementations.Exceptions;
using implementations.Interfaces;
using implementations.Models;
using implementations.Services;
using NUnit.Framework;

namespace tests
{
    public class UserServiceTests
    {
        private void PrepareTest()
        {
            var model = new Model1();
            //usuwanie bazy

            SqlConnection.ClearAllPools();
            model.Database.Delete();
            Preparator.CreateUserAndProject(model);


        }

        [Test]
        public void AdminLogin()
        {
            PrepareTest();
            IUserService userService = new UserService();

            var model = new LoginModel();
            model.Login = "mkasprzak";
            model.Password = "123456";
            int userId = userService.Login(model);
            Assert.Greater(userId, 0);
        }
        [Test]
        [ExpectedException(typeof(LoginInUseException))]
        public void RegisterLoginTest()
        {
            PrepareTest();
            IUserService userService = new UserService();
            var model = new RegisterModel();
            model.Login = "MKASPRZAK";
            model.Password = "123456";
            model.ConfirmPassword = "123456";
            model.Email = "as@wp.pl";
            model.Name = "test";

            userService.Register(model);


        }

    }
}
