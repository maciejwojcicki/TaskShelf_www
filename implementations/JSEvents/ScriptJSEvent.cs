using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace implementations.JSEvents
{
    public class ScriptJSEvent : JSEvent
    {
        public ScriptJSEvent(object data)
            : base(JSEvent.JSEventTypes.Script, data)
        { }
    }
}
