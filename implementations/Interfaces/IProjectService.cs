using core.Models;
using database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace implementations.Interfaces
{
    public interface IProjectService
    {
        List<ProjectModel> GetProjects(IPrincipal currentPrincipal);
        void SaveProject(CreateProjectModel model, IPrincipal currentPrincipal);  
    }
}
