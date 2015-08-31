using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace implementations.JSEvents
{
    public class InfoJSEvent : MessageJSEvent
    {
        public InfoJSEvent(string message)
            : base("JSEventMessage", new
            {
                Message = message,
                CssClass = "info-message",
                CloseDelay = 4000
            }) { }
    }
}
