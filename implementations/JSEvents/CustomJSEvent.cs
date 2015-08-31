using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace implementations.JSEvents
{
    public class CustomJSEvent : MessageJSEvent
    {
        public CustomJSEvent(string templateName, object data)
            : base(templateName, data)
        { }
    }
}
