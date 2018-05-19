using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Practica.Core;
using System.Data.Common;
using System.Linq;
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace Practica.Data
{
    public class StudentProfileRepository : IStudentProfileRepository
    {
        private PracticaContext _context;

        public StudentProfileRepository(PracticaContext context)
        {
            _context = context;
        }

        public StudentProfile Get(string UserId)
        {
            return _context.StudentsProfile.Where(c => c.UserId == UserId).FirstOrDefault();
        }

        public void Add(StudentProfile studentProfile)
        {
            _context.StudentsProfile.Add(studentProfile);
        }

        public void Remove(StudentProfile studentProfile)
        {
            _context.StudentsProfile.Remove(studentProfile);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
