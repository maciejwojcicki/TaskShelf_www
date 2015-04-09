using implementations.Interfaces;
using implementations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TaskShelf_www.App_Start;

namespace TaskShelf_www.Parts.User
{
    public class UserController : Controller
    {
        IUserService userService = null;

        public UserController()
        {
            userService = new UserService();
        }
        [ChildActionOnly]
        public ActionResult LoginBox()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult RegisterBox()
        {
            return View();
        }

        [AjaxOnly]
        [HttpPost]
        public ActionResult Login(core.Models.LoginModel model)
        {
            int userId = 0;
            try 
            {
                userId = userService.Login(model);
            }
            catch (implementations.Exceptions.AccountNotActivatedException)
            {
                return Json(JsonReturns.Redirect("/User/AccountNotActivated"), JsonRequestBehavior.AllowGet);
            }

            var authTicket = new FormsAuthenticationTicket(
                1,
                userId.ToString(),
                DateTime.Now,
                DateTime.Now.AddMinutes(1440),
                true,
                "",
                FormsAuthentication.FormsCookiePath);
            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(authCookie);

            return Json(JsonReturns.Redirect("/Board/Index"), JsonRequestBehavior.AllowGet);
        }
        
    }
}