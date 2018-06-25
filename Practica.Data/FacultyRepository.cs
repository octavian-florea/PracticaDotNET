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
    public class FacultyRepository : IFacultyRepository
    {
        private PracticaContext _context;

        public FacultyRepository(PracticaContext context)
        {
            _context = context;
        }


        public IEnumerable<Faculty> GetAll()
        {
            
            return _context.Faculties
                .Include(c => c.University)
                .ToList();
        }

        public IEnumerable<Faculty> GetAllByQueryName(string query)
        {
            return _context.Faculties
                .Include(c => c.University)
                .Where(c => c.Name.Contains(query) || c.University.Name.Contains(query))
                .ToList();
        }
    }
}
