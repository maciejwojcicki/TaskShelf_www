using database.Entities;
using implementations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace implementations.Interfaces
{
    public interface ILabelService
    {
        List<Label> GetLabel(int ProjectId);
        void SaveLabel(CreateLabelModel model, int ProjectId);
    }
}
