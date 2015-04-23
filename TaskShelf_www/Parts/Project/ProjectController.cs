using implementations.Interfaces;
using implementations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TaskShelf_www.App_Start;
using TaskShelf_www.Parts.Project.Model;

namespace TaskShelf_www.Parts.Project
{
    public class ProjectController : Controller
    {
        IProjectService projectService = null;
        public ProjectController()
        {
            projectService = new ProjectService();
        }

        // GET: Project
        [ChildActionOnly]
        public ActionResult Index(core.Models.ProjectModel model)
        {
            //var projectData = projectService.GetProjects(User);
            var test = projectService.GetProjects(User,model);
            return View(test);

            //return View();
        }
    }
}