using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database.Entities
{
    public class CheckListPoint
    {
        public int CheckListPointId { get; set; }
        public string Value { get; set; }
        public bool Checked { get; set; }

        public virtual CheckList Checklist { get; set; }
    }
}
