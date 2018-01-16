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
    public class UniversityRepository : IUniversityRepository
    {
        private PracticaContext _context;

        public UniversityRepository(PracticaContext context)
        {
            _context = context;
        }


        public IEnumerable<University> GetAll()
        {
            
            return _context.Universities.ToList();
        }

        public IEnumerable<University> GetAllByQueryName(string query)
        {
            return _context.Universities
                .Where(c => c.Name.Contains(query)).ToList();
        }
    }
}
