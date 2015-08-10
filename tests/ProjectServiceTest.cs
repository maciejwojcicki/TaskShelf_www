using core.Models;
using database;
using database.Entities;
using implementations.Interfaces;
using implementations.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace tests
{
    public class ProjectServiceTest
    {
        private void PrepareTest()
        {
            var model = new Model1();
            //usuwanie bazy

            SqlConnection.ClearAllPools();
            model.Database.Delete();
            Preparator.CreateUserAndProject(model);


        }

        [Test]
        public void ProjectList()
        {
            PrepareTest();

            IProjectService projectService = new ProjectService();
            var model = new Model1();

            var user = new User();
            user.UserId = 1;
            user.Login = "mkasprzak";
            user.Name = "Mateuszek";
            user.Password = "7c4a8d09ca3762af61e59520943dc26494f8941b".ToUpper();
            user.Email = "mkasp@p.pl";


            Project p = new Project();
            p.ProjectId = 1;
            p.Name = "test";
            
            
             var zzz = from o in model.Set<Project>()
                       from k in o.Users
                       where k.UserId == user.UserId
                       select new ProjectModel { Project = o };

        }



    }
}
