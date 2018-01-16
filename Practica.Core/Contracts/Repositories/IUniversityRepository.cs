using System.Collections.Generic;

namespace Practica.Core
{
    public interface IUniversityRepository
    {
        IEnumerable<University> GetAll();
        IEnumerable<University> GetAllByQueryName(string query);
    }
}