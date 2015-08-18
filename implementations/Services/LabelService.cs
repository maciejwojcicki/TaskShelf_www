using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using database;
using database.Entities;
using implementations.Interfaces;
using implementations.Models;
using implementations.Utils;

namespace implementations.Services
{
    public class LabelService:ILabelService
    {
        private Model1 context;

        public LabelService()
        {

            this.context = new Model1();
            this.context.Configuration.LazyLoadingEnabled = false;
            this.context.Database.Log = (text) =>
            {
                Console.WriteLine(text);
            };
        }
        public List<Label> GetLabel(int ProjectId)
        {
            var CurrentProject = context.Set<Project>().Single(p => p.ProjectId == ProjectId);
                          
            var model = from x in context.Set<Label>()
                        where x.Project.ProjectId.Equals(CurrentProject.ProjectId)
                        select x;

            return model.ToList();
        }

        public void SaveLabel(CreateLabelModel model,int ProjectId)
        {
            ModelUtils.Validate(model);

            var CurrentProject = context.Set<Project>().Single(p => p.ProjectId == ProjectId);

            if (model.LabelId == 0)
            {
                var label = new Label();
                label.Name = model.Name;
                label.Project = CurrentProject;
                context.Set<Label>().Add(label);
                context.SaveChanges();
            }
            else
            {
                var dbEntry = context.Set<Label>().Find(model.LabelId);
                dbEntry.Name = model.Name;
                context.SaveChanges();
            }
            

        }
    }
}
