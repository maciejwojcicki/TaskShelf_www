using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace implementations.JSEvents
{
    public class ErrorJSEvent : MessageJSEvent
    {
        public ErrorJSEvent(string message) :
            base("JSEventMessage", new
            {
                Message = message,
                CssClass = "error-message"
            }) { }
    }
}
