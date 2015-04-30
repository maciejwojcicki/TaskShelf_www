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
        public ActionResult ProjectList()
        {
            return View();                                                                                                                                                                                              
        }

        [HttpGet]
        public ActionResult Projects()
        {
            //var projectData = projectService.GetProjects(User);
            var test = projectService.GetProjects(User);
            ////return View(test);
            //ProjectModel model = new ProjectModel() { Projects = test };
            ////modell.Projects = test;

            return Json(new 
            { 
                Projects = test.Select(s=> new{
                    Name = s.Name
                }

                ),
                HasMore = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}