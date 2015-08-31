using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace implementations.JSEvents
{
    public class ViewInputValidationJSEvent : JSEvent
    {
        public ViewInputValidationJSEvent(string inputName, string message)
            : base(JSEventTypes.ViewInputValidation, new
            {
                Message = message,
                InputName = inputName
            })
        { }
    }
}
