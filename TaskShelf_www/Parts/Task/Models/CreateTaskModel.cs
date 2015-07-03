using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskShelf_www.Parts.Task.Models
{
    public class CreateTaskModel
    {
        public database.Entities.Task.TaskType Types { get; set; }
        public database.Entities.Task.TaskStatus Statuses { get; set; }


    }
}