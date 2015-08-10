using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database.Entities
{
    public class Module
    {
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public virtual Project Project { get; set; }
    }
}
