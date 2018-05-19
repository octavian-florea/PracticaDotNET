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
    public class CompanyProfileRepository : ICompanyProfileRepository
    {
        private PracticaContext _context;

        public CompanyProfileRepository(PracticaContext context)
        {
            _context = context;
        }

        public CompanyProfile Get(string UserId)
        {
            return _context.CompanyProfiles.Where(c => c.UserId == UserId).FirstOrDefault();
        }

        public void Add(CompanyProfile companyProfile)
        {
            _context.CompanyProfiles.Add(companyProfile);
        }

        public void Remove(CompanyProfile companyProfile)
        {
            _context.CompanyProfiles.Remove(companyProfile);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
