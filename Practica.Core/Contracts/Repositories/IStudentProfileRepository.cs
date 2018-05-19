using System.Collections.Generic;

namespace Practica.Core
{
    public interface IStudentProfileRepository
    {
        void Add(StudentProfile studentProfile);
        StudentProfile Get(string UserId);
        void Remove(StudentProfile studentProfile);
        bool Save();
    }
}