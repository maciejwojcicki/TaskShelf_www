using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database.Entities
{
    public class Label
    {
        public Label()
        {
            Tasks = new List<Task>();
        }
        public int LabelId { get; set; }
        public string Name { get; set; }

        public virtual Project Project { get; set; }
        public virtual List<Task> Tasks { get; set; }
    }
}
