using implementations.Interfaces;
using implementations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TaskShelf_www.App_Start;

namespace TaskShelf_www.Parts.Project
{
    public class ProjectController : Controller
    {
        IProjectService projectService = null;
        ProjectController()
        {
            projectService = new ProjectService();
        }
        // GET: Project
        [ChildActionOnly]
        public ActionResult Index()
        {
            //var projectData = projectService.GetProjects(User);
            //ViewBag.user = User;
            return View();
        }
    }
}