using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core.Models;
using System.Security.Principal;
using database.Entities;
namespace implementations.Interfaces
{
    public interface IUserService
    {
        int Login(LoginModel model);
        IPrincipal GetPrincipal(int userId);
        User GetCurrentUser(IPrincipal currentPrincipal);
        void Register(RegisterModel model);
    }
}
