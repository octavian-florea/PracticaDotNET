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
    public class ActivityRepository : IActivityRepository
    {
        private PracticaContext _context;

        public ActivityRepository(PracticaContext context)
        {
            _context = context;
        }

        public Activity Get(int id)
        {
            return _context.Activities.Where(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<Activity> GetAll()
        {
            
            return _context.Activities.ToList();
        }


        public IEnumerable<Activity> GetAllByUser(string userid)
        {
            return _context.Activities
                .Where(c => c.UserId == userid).ToList();
        }

        public IEnumerable<Activity> Find(ActivityFilter activityFilter)
        {

            return _context.Activities.ToList();
        }

        public void Add(Activity activity)
        {
            _context.Activities.Add(activity);
        }

        public void Remove(Activity activity)
        {
            _context.Activities.Remove(activity);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
