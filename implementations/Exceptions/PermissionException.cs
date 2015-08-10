using database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace implementations.Exceptions
{
    public class PermissionException : Exception
    {
        private Permission.PermissionList permission;
        public PermissionException(Permission.PermissionList permission)
        {
            this.permission = permission;
        }
    }
}
