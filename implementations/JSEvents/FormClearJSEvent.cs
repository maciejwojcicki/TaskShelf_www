using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace implementations.JSEvents
{
    public class FormClearJSEvent : JSEvent
    {
        public FormClearJSEvent(string dataFormId)
            : base(JSEventTypes.FormClear, new
            {
                DataFormId = dataFormId
            })
        { }
    }
}
