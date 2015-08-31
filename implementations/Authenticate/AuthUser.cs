using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace implementations.Authenticate
{
    public class AuthUser : GenericPrincipal
    {
        private string name;
        private string[] roles;
        private int id;
        //private bool isCompleted, isActivated;
        private string authCookieValue;

        public int ID
        {
            get { return this.id; }
        }

        [Obsolete]
        public string Name
        {
            get { return this.name; }
        }

        //public bool IsActivated
        //{
        //    get { return this.isActivated; }
        //}

        public string[] Roles
        {
            get { return this.roles; }
        }

        public string AuthCookieValue
        {
            get { return this.authCookieValue; }
        }

        public AuthUser(int id, string name, bool isCompleted, bool isActivated, string[] roles, string authCookieValue)
            : base(new GenericIdentity(name), roles)
        {
            this.id = id;
            this.name = id.ToString();
            this.roles = roles;
            this.authCookieValue = authCookieValue;
            //this.isActivated = isActivated;
        }

        public static IPrincipal Create(CookieAuthUser cookieAuthUser, string authCookieValue)
        {
            return new AuthUser(cookieAuthUser.ID, cookieAuthUser.ID.ToString(), cookieAuthUser.IsCompleted, cookieAuthUser.IsActivated, cookieAuthUser.Roles, authCookieValue);
        }

    }
}
