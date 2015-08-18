using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using database.Entities;
using implementations.Models;

namespace implementations.Interfaces
{
    public interface ITaskService
    {
        List<Task> GetTasks(IPrincipal CurrentPrincipal, int projectId);
        void SaveTask(CreateTaskModel model,int projectId);
        List<TaskComment> GetComments(IPrincipal CurrentPrincipal, int taskId);
        TaskModel CurrentTask(int taskId);
        void SaveComent(CreateCommentModel model, IPrincipal CurrentPrincipal, int taskId);
        
    }
}
