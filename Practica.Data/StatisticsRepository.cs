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
    public class StatisticsRepository : IStatisticsRepository
    {
        private PracticaContext _context;

        public StatisticsRepository(PracticaContext context)
        {
            _context = context;
        }

        public int GetNumberOfActiveActivities(string activityType)
        {
            return _context.Activities
              .Where(c => c.PublishDate <= DateTime.Today && c.ExpirationDate > DateTime.Today && c.Type.Equals(activityType))
              .Count();
        }

        public int GetNumberOfCompanies()
        {
            return _context.CompanyProfiles.Count();
        }

        public int GetNumberOfStudents()
        {
            return _context.StudentsProfile.Count();
        }
    }
}
