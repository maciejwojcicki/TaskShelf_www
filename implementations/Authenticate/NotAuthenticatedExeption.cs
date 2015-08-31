using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace implementations.Authenticate
{
    public class NotAuthenticatedExeption : Exception
    {
        public NotAuthenticatedExeption()
            : base()
        {

        }
    }
}
