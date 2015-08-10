using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database.Entities
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }

        public Project Project { get; set; }
        public User User { get; set; }
    }
}
