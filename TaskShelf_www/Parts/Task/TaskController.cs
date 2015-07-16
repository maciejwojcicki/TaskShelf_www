using implementations.Interfaces;
using implementations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskShelf_www.App_Start;
using TaskShelf_www.Helpers;
using TaskShelf_www.Parts.Task.Models;

namespace TaskShelf_www.Parts.Task
{
    public class TaskController : Controller
    {
        ITaskService taskService = null;
        
        public TaskController()
        {
            taskService = new TaskService();
        }
        // GET: Task
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TaskListView()
        {

            return View();
        }
        public ActionResult CreateTaskView()
        {
            return View();
        }
        public ActionResult CreateTaskModel()
        {
           
           
            var values = EnumHelper.GetValues<database.Entities.Task.TaskStatus>().Select(x => new
            {
                Name = EnumHelper.GetEnumDescription(x),
                Value = (int)x
            });
            return Json(values, JsonRequestBehavior.AllowGet);

        }
        public ActionResult TaskList()
        {
            var projectId = Int32.Parse(Request.Cookies["ProjectId"].Value);
            var task = taskService.GetTasks(User, projectId);

            return Json(new
            {
                 Tasks = task.Select(s => new
                {
                    Name = s.Name
                }

                ),
                HasMore = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult CreateTask(core.Models.CreateTaskModel model)
        {
            var projectId = Int32.Parse(Request.Cookies["ProjectId"].Value);
            taskService.CreateTask(model, projectId);

            return Json(JsonReturns.Redirect("/Task/Index"), JsonRequestBehavior.AllowGet);
        }
    }
}