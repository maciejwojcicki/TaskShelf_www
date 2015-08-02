using TaskShelf_www.App_Start;
using TaskShelf_www.Parts;
using implementations.Services;
using implementations.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Xml;


namespace TaskShelf_www
{   // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            log4net.Config.XmlConfigurator.Configure();
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //ModelBinders.Binders.Add(new KeyValuePair<Type, IModelBinder>(typeof(core.Models.BoardPostPublishModel.Attachment), new BoardPostAttachmentBinder()));

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CustomRazorViewEngine());

            var languagePath = Server.MapPath("/Languages/" +
                System.Configuration.ConfigurationManager.AppSettings["Language"] +
                ".lang.xml");

            if (File.Exists(languagePath))
            {
                var items = 0;
                var watch = Stopwatch.StartNew();
                using (Stream stream = File.Open(languagePath, FileMode.Open))
                {
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.Async = true;
                    using (XmlReader reader = XmlReader.Create(stream, settings))
                    {
                        while (reader.Read())
                        {
                            if (reader.Name == "item")
                            {
                                var name = reader.GetAttribute("name");
                                var content = reader.ReadElementContentAsString();
                                HttpContext.Current.Cache["_language_" + name] = content;
                                items++;
                            }
                        }
                    }
                }
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                log4net.LogManager.GetLogger("Global.asax.cs").Debug("Loaded " + items + " language keys in " + elapsedMs + " ms.");
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var httpContext = ((MvcApplication)sender).Context;
            Exception ex = Server.GetLastError();
            httpContext.Response.Clear();
            httpContext.ClearError();

            httpContext.Response.TrySkipIisCustomErrors = true;

            Response.StatusCode = 500;

            log4net.LogManager.GetLogger("Global.asax.cs").Error(ex.Message, ex);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                httpContext.Response.ContentType = "application/json";

                object errorObject = null;

                ValidationException validationException = ex as ValidationException;
                if (validationException != null)
                {
                    errorObject = JsonReturns.ValidationError(
                        validationException.ValidationResult.MemberNames.First(),
                        validationException.ValidationResult.ErrorMessage);
                }
                else
                {
                    errorObject = JsonReturns.Error(ex.Message);
                }
                httpContext.Response.Write(
                       Newtonsoft.Json.JsonConvert.SerializeObject(
                       errorObject));
                return;
            }

            var routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("action", "Index");
            IController errorController = new ErrorController();
            errorController.Execute(new RequestContext(
                new HttpContextWrapper(Context), routeData));

            httpContext.Response.Flush();
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                if (!string.IsNullOrEmpty(authCookie.Value))
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    int userId = int.Parse(authTicket.Name);
                    IUserService userService = new UserService();
                    try
                    {
                        HttpContext.Current.User = userService.GetPrincipal(userId);
                        Response.Cookies.Add(authCookie);
                        
                    }
                    catch (Exception ex)
                    {
                        log4net.LogManager.GetLogger("Global.asax.cs").Error("User not found!", ex);
                        FormsAuthentication.SignOut();
                        Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
                        HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(""), null);
                    }
                }
            }
        }
    }
    
}
