using database;
using database.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests
{
    public class DbTest
    {
        private static void PrepareTest()
        {
            var model = new Model1();
            //usuwanie bazy

            SqlConnection.ClearAllPools();
            model.Database.Delete();
            Preparator.CreateUserAndProject(model);
        }

        [Test]
        public void TestDataTest()
        {
            PrepareTest();
        }
        [Test]
        public void DatabaseTest()
        {
            
            var model = new Model1();

            model.Set<User>().Add(new User()
            {
                Login = "ssiwiak",
                Name="Sylwia",
                Password="slonczeko"
            });
            model.SaveChanges();
        }
                
            
            


        
    }
}
