using database.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace database
{
    public class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
            //SqlConnection.ClearAllPools();
            //Database.SetInitializer<Model1>(null);
            Database.SetInitializer<Model1>(new DropCreateDatabaseAlways<Model1>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ConfigureUser(modelBuilder);
            ConfigureProject(modelBuilder);
            ConfigureTask(modelBuilder);
            ConfigurePermission(modelBuilder);
            ConfigureCheckList(modelBuilder);

        }

        private void ConfigureUser(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(h => h.UserId)
                .HasMany(h => h.Projects)
                .WithMany(w => w.Users);
        
            //modelBuilder.Entity<User>()
            //    .HasMany(h=>h.Permissions)
            //    .WithMany(w=>w.Users);

            modelBuilder.Entity<User>()
                .HasMany(h => h.UserPermissions)
                .WithRequired(w => w.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(h => h.Groups)
                .WithRequired(w => w.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(h => h.TaskComments)
                .WithRequired(w => w.User)
                .WillCascadeOnDelete(false);

        }
        private void ConfigureProject(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasKey(h => h.ProjectId)
                .HasMany(h => h.Tasks)
                .WithRequired(w => w.Project)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(h => h.UserPermissions)
                .WithRequired(w => w.Project)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(h => h.Labels)
                .WithRequired(w => w.Project)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(h => h.Groups)
                .WithRequired(w => w.Project)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(h => h.Modules)
                .WithRequired(w => w.Project)
                .WillCascadeOnDelete(false);
        }

        private void ConfigurePermission(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>()
                .HasKey(h => h.PermissionId)
                .HasMany(h => h.UserPermissions)
                .WithRequired(w => w.Permission)
                .WillCascadeOnDelete(false);
        }
        private void ConfigureTask(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .HasKey(h => h.TaskId)
                .HasOptional(h => h.CheckList)
                .WithRequired(w => w.Task)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Task>()
                .HasMany(h => h.TaskComments)
                .WithRequired(w => w.Task)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Task>()
                .HasKey(h=>h.TaskId)
                .HasMany(h => h.Labels)
                .WithMany(w => w.Tasks);

            modelBuilder.Entity<Task>()
                .HasMany(h => h.TaskAttachments)
                .WithRequired(w => w.Task)
                .WillCascadeOnDelete(false);
                

        }
        private void ConfigureCheckList(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CheckList>()
                .HasKey(h => h.CheckListId)
                .HasMany(h => h.CheckListPoints)
                .WithRequired(w => w.Checklist)
                .WillCascadeOnDelete(false);
        }
    }
}
