using implementations.Interfaces;
using implementations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace implementations.Authenticate
{
    public class AppAuthorizeAttribute : AuthorizeAttribute
    {
        private bool onlyAuthenticate;
        IUserService userService = null;
        public AppAuthorizeAttribute(bool onlyAuthenticate = false)
        {
            this.onlyAuthenticate = onlyAuthenticate;
            userService = new UserService();
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {

            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                throw new NotAuthenticatedExeption();
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var authUser = filterContext.HttpContext.User as AuthUser;
            if (authUser != null)
            {
                var authuserr = authUser.ID;
                var autha = authUser.Name;
                var appController = filterContext.Controller as AppController;
                if (appController != null)
                {
                    var database = appController.GetDatabase();

                    var user = userService.GetUserById(authUser.ID);
                    if (user != null)
                    {
                        if (user.UserId == null)
                        {
                            // httpContext.Response.Redirect("/Sciezka");                     
                            filterContext.Result = new RedirectResult("/Sciezka");
                        }
                    }
                }
            }
            base.OnAuthorization(filterContext);
        }

    }
}
