using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database.Entities
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public int ExpectedWorkTime { get; set; }
        public DateTime? CompletedDate { get; set; }
        public TaskStatus Status { get; set; }
        public TaskType Type { get; set; }

        public virtual Project Project { get; set; }
        public virtual List<TaskAttachment> TaskAttachments { get; set; }
        public virtual CheckList CheckList { get; set; }
        public virtual List<TaskComment> TaskComments { get; set; }
        public virtual List<Label> Labels { get; set; }
        

        public enum TaskStatus:int
        {
            [Description("Otwarty")]
            Open = 1,
            [Description("W trakcie")]
            InProgress = 2,
            [Description("Zakończony")]
            Completed = 3
        }

        public enum TaskType:int
        {
            [Description("Błąd")]
            Error = 1,
            [Description("Test")]
            Test = 2,
            [Description("Zadanie")]
            Refactor = 3,
            [Description("Nowe wymaganie")]
            Story = 4
        }
    }
}
