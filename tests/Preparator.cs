using database;
using database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tests
{
    public class Preparator
    {
        public static void CreateUserAndProject (Model1 model)
        {
            //uzytkownik
            var user = new User();
            user.Login = "mkasprzak";
            user.Name = "Mateuszek";
            user.Password = "7c4a8d09ca3762af61e59520943dc26494f8941b".ToUpper();
            user.Email = "mkasp@p.pl";
            model.Set<User>().Add(user);
            model.SaveChanges();

            //uprawnienie
            var permission = new Permission();
            permission.Value = Permission.PermissionList.CanCreate;
            permission.Name = Permission.PermissionList.CanCreate.ToString();
            model.Set<Permission>().Add(permission);
            model.SaveChanges();

            //user.Permissions = new List<Permission>
            //{
            //    permission
            //};
            //model.SaveChanges();
            //Projekt
            var project = new Project();
            project.Name = "test";            
            model.Set<Project>().Add(project);
            model.SaveChanges();
            
            //Rekord Uzytkownik_Projekt
            user.Projects = new List<Project>
            {
                project
            };
            model.SaveChanges();

            var userPermission = new UserPermission()
            {
                
                Permission = permission,
                Project = project,
                User = user
            };
            model.Set<UserPermission>().Add(userPermission);
            model.SaveChanges();

            var task = new Task();
            task.Name = "Błąd wyszukiwarki";
            task.Status = Task.TaskStatus.Open;
            task.Type = Task.TaskType.Error;
            task.CreateDate = DateTime.Now;
            task.ExpectedWorkTime = 8;
            task.Project = project;
            model.Set<Task>().Add(task);
            model.SaveChanges();

            var label = new Label();
            label.Name = "label";
            label.Project = project;
            model.Set<Label>().Add(label);
            model.SaveChanges();

            label.Tasks = new List<Task>
            {
                task
            };
            model.SaveChanges();


            var comment = new TaskComment();
            comment.Text = "reakcja na akcje";
            comment.CreateDate = DateTime.Now;
            comment.Task = task;
            comment.User = user;
            model.Set<TaskComment>().Add(comment);
            model.SaveChanges();
          
        }

      
    }
}
