using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace implementations.ActionResults
{
    public class JSEventsResult : System.Web.Mvc.ActionResult
    {
        private List<JSEvents.JSEvent> events = new List<JSEvents.JSEvent>();

        public JSEventsResult(JSEvents.JSEvent @event)
        {
            // TODO: Complete member initialization
            this.events.Add(@event);
        }

        public JSEventsResult(IEnumerable<JSEvents.JSEvent> events)
        {
            // TODO: Complete member initialization
            this.events.AddRange(events);
        }
        public override void ExecuteResult(System.Web.Mvc.ControllerContext context)
        {
            HttpContextBase httpContextBase = context.HttpContext;
            httpContextBase.Response.Buffer = true;
            httpContextBase.Response.Clear();

            httpContextBase.Response.ContentType = "application/json";

            var serializedEvents = Newtonsoft.Json.JsonConvert.SerializeObject(this.events);
            httpContextBase.Response.Write(serializedEvents);
        }
    }
}
