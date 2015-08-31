using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace implementations.JSEvents
{
    public class NotAuthenticatedPopupJSEvent : JSEvent
    {
        public NotAuthenticatedPopupJSEvent(string url, string data)
            : base(JSEventTypes.NotAuthenticated, new
            {
                Url = url,
                Data = data
            }) { }
    }
}
