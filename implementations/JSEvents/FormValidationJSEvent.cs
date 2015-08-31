using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace implementations.JSEvents
{
    public class FormValidationJSEvent : JSEvent
    {
        public FormValidationJSEvent(string dataFormId, string inputName, string message)
            : base(JSEventTypes.FormValidation, new
            {
                DataFormId = dataFormId,
                Message = message,
                InputName = inputName
            })
        { }
    }
}
