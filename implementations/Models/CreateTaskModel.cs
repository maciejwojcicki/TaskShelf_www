using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using database.Entities;
using System.ComponentModel.DataAnnotations;
namespace implementations.Models
{
    public class CreateTaskModel
    {
        public int TaskId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public int ExpectedWorkTime { get; set; }
        public database.Entities.Task.TaskStatus Status { get; set; }
        public database.Entities.Task.TaskType Type { get; set; }
        public List<TaskAttachment> Attachments { get; set; }
    }
}
