using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using database.Entities;
using implementations.Models;

namespace implementations.Interfaces
{
    public interface IProjectService
    {
        List<ProjectModel> GetProjects(IPrincipal currentPrincipal);
        void SaveProject(CreateProjectModel model, IPrincipal currentPrincipal);  
    }
}
