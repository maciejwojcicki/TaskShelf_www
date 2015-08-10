using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database.Entities
{
    public class User
    {
        public User()
        {
            Projects = new List<Project>();
            //Permissions = new List<Permission>();
            UserPermissions = new List<UserPermission>();
            Groups = new List<Group>();
            TaskComments = new List<TaskComment>();

        }
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ActivationToken { get; set; }

        public virtual List<Project> Projects { get; set; }
        //public virtual List<Permission> Permissions { get; set; }
        public virtual List<UserPermission> UserPermissions { get; set; }
        public virtual List<Group> Groups { get; set; }
        public virtual List<TaskComment> TaskComments { get; set; }
    }
}
