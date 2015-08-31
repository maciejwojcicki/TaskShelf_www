using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace implementations.JSEvents
{
    public class JSEvent
    {
        private JSEventTypes type;
        public String Type { get { return this.type.ToString(); } }

        private object data;
        public dynamic Data
        {
            get
            {
                if (this.data == null)
                {
                    this.data = new object();
                }
                return this.data;
            }
        }

        public JSEvent(JSEventTypes type, object data)
        {
            this.type = type;
            this.data = data;
        }

        public enum JSEventTypes
        {
            Message,
            FormValidation,
            FormClear,
            TopLocation,
            Script,
            ViewInputValidation,
            NotAuthenticated
        }
    }
}
