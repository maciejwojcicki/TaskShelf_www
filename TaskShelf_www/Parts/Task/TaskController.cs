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
        public ActionResult TaskReview(int TaskId)
        {
            TaskReviewModel model = new TaskReviewModel();
            model.TaskId = TaskId;
            
            return View(model);
        }


        public ActionResult CreateTaskView()
        {
            CreateTaskModel model = new CreateTaskModel();

            Dictionary<int, string> types = new Dictionary<int, string>();

            foreach (var item in EnumHelper.GetValues<database.Entities.Task.TaskType>())
            {
                types.Add((int)item, EnumHelper.GetEnumDescription(item));
            }

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
                        TaskId = s.TaskId,
                        Name = s.Name
                    }),
                    HasMore = true
                }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult CreateTask(implementations.Models.CreateTaskModel model)
        {
            var projectId = Int32.Parse(Request.Cookies["ProjectId"].Value);

            taskService.SaveTask(model, projectId);

            return Json(JsonReturns.Redirect("/Task/Index"), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AjaxOnly]
        public ActionResult UploadAttachment(AttachmentModel model)
        {
            var projectId = Int32.Parse(Request.Cookies["ProjectId"].Value);
            

            return Json(JsonReturns.Redirect("/Task/Index"), JsonRequestBehavior.AllowGet);
        }

        [Ajax]
        public ActionResult TaskReviewModel(int taskId)
        {
           
            var Task = taskService.CurrentTask(taskId);
            var Comments = taskService.GetComments(User, taskId);
            var Attachments = taskService.GetAttachments(taskId);
            
            return Json(new
                {
                    TaskId = Task.TaskId,
                    Name = Task.Name,
                    Description = Task.Description,
                    CreateDate = Task.CreateDate.ToString(),
                    ExpectedWorkTime = Task.ExpectedWorkTime,
                    CompletedDate = Task.CompletedDate,
                    Status = Task.Status,
                    Type = Task.Type,
                    Attachments = Attachments.Select(s => new
                    {
                        AttachmentId = s.TaskAttachmentId,
                        FileName = s.FileName
                    }),
                    Comments = Comments.Select(s => new
                    {
                        CommentId = s.TaskCommentId,
                        Text = s.Text
                    })
                }, JsonRequestBehavior.AllowGet);
        }
    }
}