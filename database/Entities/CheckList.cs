using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database.Entities
{
    public class CheckList
    {
        public int CheckListId { get; set; }
        public string Name { get; set; }

        public virtual Task Task { get; set; }
        public virtual List<CheckListPoint> CheckListPoints { get; set; }
    }
}
