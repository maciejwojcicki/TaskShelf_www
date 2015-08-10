using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database.Entities
{
    public class UserPermission
    {
        public int UserPermissionId { get; set; }

        public virtual User User { get; set; }
        public virtual Project Project { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
