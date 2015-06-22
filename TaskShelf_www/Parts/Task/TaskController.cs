using implementations.Interfaces;
using implementations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskShelf_www.Parts.Task
{
    public class TaskController : Controller
    {
        ITaskService taskService = null;

        TaskController()
        {
            taskService = new TaskService();
        }
        // GET: Task
        public ActionResult TaskList(int projectId)
        {
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
    }
}