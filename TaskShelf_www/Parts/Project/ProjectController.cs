using implementations.Interfaces;
using implementations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TaskShelf_www.App_Start;
using TaskShelf_www.Parts.Project.Models;

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
    
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult ProjectListView()
        {
            return View();                                                                                                                                                                                              
        }
        [ChildActionOnly]
        public ActionResult CreateProjectView()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ProjectList()
        {
            var test = projectService.GetProjects(User);

            return Json(new 
            { 
                Projects = test.Select(s=> new{
                    ProjectId = s.Project.ProjectId,
                    Name = s.Project.Name,
                    Url = System.Configuration.ConfigurationManager.AppSettings["Domain"] + "Task/Index"
                }

                ),
                HasMore = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult CreateProject(core.Models.CreateProjectModel model)
        {
            projectService.CreateProject(model, User);

            return Json(JsonReturns.Redirect("/Project/Index"), JsonRequestBehavior.AllowGet);
        }
    }
}