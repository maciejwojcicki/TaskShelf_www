using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace implementations.Authenticate
{
    public class CookieAuthUser
    {

        public CookieAuthUser()
        { }

        public CookieAuthUser(int id, string name, bool isCompleted, bool isActivated, string[] roles)
        {
            this.ID = id;
            this.Name = name;
            this.Roles = roles;
            this.IsCompleted = isCompleted;
            this.IsActivated = isActivated;
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public string[] Roles { get; set; }

        public bool IsCompleted { get; set; }

        public bool IsActivated { get; set; }

        public HttpCookie GetAuthCookie()
        {
            var serializer = new JavaScriptSerializer();
            var data = serializer.Serialize(this);
            var authTicket = new FormsAuthenticationTicket(
                1,
                this.Name,
                DateTime.Now,
                DateTime.Now.AddMinutes(1440),
                true,
                data,
                FormsAuthentication.FormsCookiePath);
            string encTicket = FormsAuthentication.Encrypt(authTicket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            cookie.Expires = authTicket.Expiration;
            return cookie;
        }
    }
}
