using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database.Entities
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string Name { get; set; }
        public PermissionList Value { get; set; }
        
        //public virtual List<User> Users { get; set; }
        public virtual List<UserPermission> UserPermissions { get; set; }
        public enum PermissionList
        {
            
            [Description("CanLogin")]
            CanCreate,
            CanInvite
        }

        
    }
}
