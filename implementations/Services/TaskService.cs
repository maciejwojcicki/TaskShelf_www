using database;
using database.Entities;
using implementations.Interfaces;
using implementations.Models;
using implementations.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;


namespace implementations.Services
{
    public class TaskService : ITaskService  
    {
        private Model1 context;
        IUserService userService = null;

        public TaskService()
        {
            this.context = new Model1();
            this.context.Configuration.LazyLoadingEnabled = false;
            this.context.Database.Log = (text) =>
            {
                Console.WriteLine(text);
            };

            userService = new UserService();
        }
        #region TASK
        public List<Task> GetTasks(IPrincipal CurrentPrincipal, int projectId)
        {
            var CurrentProject = context.Set<Project>().Single(p => p.ProjectId == projectId);
      
            var model = context.Set<Task>().Where(p=>p.Project.ProjectId.Equals(CurrentProject.ProjectId)).ToList();
            
            return model;
        }
        public TaskModel CurrentTask(int taskId)
        {
            TaskModel model = new TaskModel();
            var CurrentTask = context.Set<Task>().Single(p => p.TaskId == taskId);

            model.Name = CurrentTask.Name;
            model.Status = CurrentTask.Status;
            model.TaskId = CurrentTask.TaskId;
            model.CreateDate = CurrentTask.CreateDate;
            model.Description = CurrentTask.Description;
            model.ExpectedWorkTime = CurrentTask.ExpectedWorkTime;
            model.Type = CurrentTask.Type;
            model.Attachments = CurrentTask.TaskAttachments;

            return model;
        }

        public void SaveTask(CreateTaskModel model,int projectId)
        {
            ModelUtils.Validate(model);

            var CurrentProject = context.Set<Project>().Single(p => p.ProjectId == projectId);
            if (model.TaskId == 0)
            {
                var task = new Task();
                task.Name = model.Name;
                task.Description = model.Description;
                task.CreateDate = DateTime.Now;
                task.ExpectedWorkTime = model.ExpectedWorkTime;
                task.Status = Task.TaskStatus.Open;
                task.Type = model.Type;
                task.Project = CurrentProject;

                context.Set<Task>().Add(task);
                context.SaveChanges();
                var test = task.TaskId;
                var select = context.Set<Task>().Find(task.TaskId);
                var attachment = new TaskAttachment();
                if (model.Attachments != null) 
                { 
                    foreach (var item in model.Attachments)
                    {
                        attachment.Task = select;
                        attachment.FileName = item.FileName;
                        context.Set<TaskAttachment>().Add(attachment);
                        context.SaveChanges();

                    }
                }
            }
            else
            {
                var DbEntry = context.Set<Task>().Find(model.TaskId);
                DbEntry.Name = model.Name;
                DbEntry.Description = model.Description;
                DbEntry.ExpectedWorkTime = model.ExpectedWorkTime;
                DbEntry.Status = model.Status;
                DbEntry.Type = model.Type;
                if (model.Attachments != null) 
                { 
                    foreach (var item in model.Attachments)
                    {
                        item.Task = DbEntry;
                        context.Set<TaskAttachment>().Add(item);
                        context.SaveChanges();
                    }
                }
            }
            
        }
        #endregion
        #region Comments
        public List<TaskComment> GetComments(IPrincipal CurrentPrincipal, int taskId)
        {
            Task CurrentTask = context.Set<Task>().Single(p => p.TaskId == taskId);
            User CurrentUser = userService.GetCurrentUser(CurrentPrincipal);

            var model = from x in context.Set<TaskComment>()
                        where x.Task.TaskId.Equals(taskId) &&
                        x.User.UserId.Equals(CurrentUser.UserId)
                        select x;
            return model.ToList();
        }

        public void SaveComent(CreateCommentModel model, IPrincipal CurrentPrincipal, int taskId)
        {

        }
        #endregion
    }
}
