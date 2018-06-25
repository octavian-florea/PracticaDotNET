using System.Collections.Generic;

namespace Practica.Core
{
    public interface IFacultyRepository
    {
        IEnumerable<Faculty> GetAll();
        IEnumerable<Faculty> GetAllByQueryName(string query);
    }
}