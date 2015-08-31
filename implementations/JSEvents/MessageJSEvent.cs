using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace implementations.JSEvents
{
    abstract public class MessageJSEvent : JSEvent
    {
        public MessageJSEvent(string templateName, object data)
            : base(JSEvent.JSEventTypes.Message, GetData(templateName, data))
        { }

        static private object GetData(string templateName, object data)
        {
            if (data == null)
            {
                data = new object();
            }
            var type = data.GetType();
            var containsTemplateName = type.GetProperties()
                .Select(n => n.Name).Contains("TemplateName");
            if (!containsTemplateName)
            {
                var dictionary = new Dictionary<string, object>();
                foreach (var property in type.GetProperties())
                {
                    dictionary.Add(property.Name, property.GetValue(data, null));
                }
                dictionary.Add("TemplateName", templateName);
                return (object)dictionary;
            }
            return data;
        }
    }
}
