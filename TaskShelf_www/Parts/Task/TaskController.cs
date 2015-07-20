using implementations.Interfaces;
using implementations.Services;
using Newtonsoft.Json;
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
            CreateTaskModel model = new CreateTaskModel();

            //var statuses = EnumHelper.GetValues<database.Entities.Task.TaskStatus>();

         
            Dictionary<int, string> types = new Dictionary<int, string>();

            foreach (var item in EnumHelper.GetValues<database.Entities.Task.TaskType>())
            {
                types.Add((int)item, EnumHelper.GetEnumDescription(item));
            }
            //EnumHelper.ToDictionary(database.Entities.Task.TaskStatus.Completed);
            //var json = new
            //{
            //    TaskStatuses = statuses.Select(s => new
            //    {
            //        Name = EnumHelper.GetEnumDescription(s),
            //        Value = (int)s
            //    })
            //};
            //Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            model.Types = types;
            return View(model);
        }
        
        public ActionResult CreateTaskModel()
        {
            var statuses = EnumHelper.GetValues<database.Entities.Task.TaskStatus>();

            return Json(new
            {
                TaskStatuses = statuses.Select(s => new
                {
                    Name = EnumHelper.GetEnumDescription(s),
                    Value = (int)s
                })
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TaskList()
        {
            var projectId = Int32.Parse(Request.Cookies["ProjectId"].Value);
            var GetTasks = taskService.GetTasks(User, projectId);

            return Json(new
            {
                Tasks = GetTasks.Select(s => new
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