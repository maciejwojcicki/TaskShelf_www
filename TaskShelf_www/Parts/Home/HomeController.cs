using implementations.Interfaces;
using implementations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskShelf_www.Parts.Home
{
    public class HomeController : Controller
    {
        IUserService userService = null;
        public HomeController()
        {
            userService = new UserService();
        }
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            if (userService.GetCurrentUser(User) != null)
               return RedirectToAction("Index", "Project");
            else
                return View();
        }
        
    }
}