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
        ILabelService labelService = null;
        IUserService userService = null;
        public ProjectController()
        {
            projectService = new ProjectService();
            labelService = new LabelService();
            userService = new UserService();
        }

        // GET: Project
    
        public ActionResult Index()
        {
            
            return View();
        }
        #region Project
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
            var GetProjects = projectService.GetProjects(User);

            return Json(new 
            {
                Projects = GetProjects.Select(s => new
                {
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
        public ActionResult CreateProject(implementations.Models.CreateProjectModel model)
        {
            projectService.SaveProject(model, User);

            return Json(JsonReturns.Redirect("/Project/Index"), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Labels
        public ActionResult LabelListView()
        {

            return View();
        }
        [HttpGet]
        public ActionResult LabelList()
        {
            var projectId = Int32.Parse(Request.Cookies["ProjectId"].Value);
            var GetLabels = labelService.GetLabel(projectId);
            return Json(new
            {
                Labels = GetLabels.Select(s => new
                {
                    Name = s.Name,
                }

                ),
                HasMore = true
            }, JsonRequestBehavior.AllowGet);
               
        }

        public ActionResult CreateLabelView()
        {
            return View();
        }

        public ActionResult CreateLabel(implementations.Models.CreateLabelModel model)
        {
            var projectId = Int32.Parse(Request.Cookies["ProjectId"].Value);
            labelService.SaveLabel(model, projectId);
            return Json(JsonReturns.Redirect("/Task/Index"), JsonRequestBehavior.AllowGet);

        }
        #endregion
    }
}