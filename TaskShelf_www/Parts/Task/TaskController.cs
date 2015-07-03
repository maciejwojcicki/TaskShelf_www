using implementations.Interfaces;
using implementations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskShelf_www.App_Start;
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
            CreateTaskModel model = new CreateTaskModel();

            List<database.Entities.Task.TaskStatus> test = new List<database.Entities.Task.TaskStatus>();
            //test.Add(model.Statuses.);
                foreach(var item in test)
                {
                    item.ToString();
                }
            foreach(var value in Enum.GetValues(typeof(database.Entities.Task.TaskStatus)))
            {
                value.ToString();
            }
                //var value in Enum.GetValues(typeof(SignMagnitude)
                //Language[] result = (Language[])Enum.GetValues(typeof(Language))

            return View();
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