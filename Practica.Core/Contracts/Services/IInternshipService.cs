using System;
using System.Collections.Generic;
using System.Text;

namespace Practica.Core
{
    public interface IInternshipService
    {
        Internship GetById(int id);
        IEnumerable<Internship> GetAll();
        Internship Create(Internship internship);
        bool Update(Internship internship);
        bool Delete(int id);
    }
}
