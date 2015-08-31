using implementations.JSEvents;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using implementations.ActionResults;
using System.Runtime.InteropServices;
using database;


namespace implementations.Authenticate
{
     public class AppController : System.Web.Mvc.Controller
    {
         private Model1 context;

        public Model1 GetDatabase()
        {
            return context;
        }

        public AppController()
        {
            this.context = new Model1();     
        }

        private List<JSEvent> jsEvents;

        public List<JSEvent> JSEvents
        {
            get
            {
                if (this.jsEvents == null)
                {
                    this.jsEvents = new List<JSEvent>();
                }
                return this.jsEvents;
            }
        }

        public void AddJSEvent(JSEvent jsEvent)
        {
            if (this.jsEvents == null)
            {
                this.jsEvents = new List<JSEvent>();
            }
            this.jsEvents.Add(jsEvent);
        }

        public JSEventsResult JSEventsResponse(JSEvent jsEvent)
        {
            return new JSEventsResult(jsEvent);
        }

        public JSEventsResult JSEventsResponse(IEnumerable<JSEvent> jsEvents)
        {
            return new JSEventsResult(jsEvents);
        }

        public List<KeyValuePair<string, string>> GetInvalidFields(ModelStateDictionary modelstate)
        {
            var invalidFields = new List<KeyValuePair<string, string>>();

            var keys = modelstate.Keys.ToList();
            var values = modelstate.Values.ToList();

            for (var i = 0; i < modelstate.Keys.Count; i++)
            {
                var hasError = values[i].Errors.Count > 0;
                if (hasError)
                {
                    var invalidField = new KeyValuePair<string, string>(
                        keys[i],
                        values[i].Errors.First().ErrorMessage);

                    invalidFields.Add(invalidField);
                }
            }

            return invalidFields;
        }

        public AuthUser AuthUser
        {
            get
            {
                var authUser = User as AuthUser;
                if (authUser == null)
                {
                    return null;
                }
                return authUser;
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            //ViewBag.IQPoints = 200;
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (database.QueryManager.Transaction != null)
        //        {
        //            if (IsInException())
        //            {
        //                database.QueryManager.Rollback();
        //            }
        //            else
        //            {
        //                database.QueryManager.Commit();
        //            }
        //        }
        //    }

        //    base.Dispose(disposing);
        //}

        public static Boolean IsInException()
        {
            return Marshal.GetExceptionPointers() != IntPtr.Zero || Marshal.GetExceptionCode() != 0;
        }
    }
}
