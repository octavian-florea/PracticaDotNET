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
            return _context.Activities
               .Include(c => c.PracticaUser.CompanyProfile)
               .Where(c => passFilterActivity(c, activityFilter))
               .ToList();
        }

        private bool passFilterActivity(Activity activity, ActivityFilter activityFilter)
        {
            if (activity.PublishDate == null && activity.ExpirationDate == null)
                return false;
            if (activity.PublishDate > DateTime.Today || activity.ExpirationDate <= DateTime.Today)
                return false;
            if (!String.IsNullOrEmpty(activityFilter.City) && !activity.City.Equals(activityFilter.City))
                return false;
            if (!String.IsNullOrEmpty(activityFilter.SearchKey) 
                && activity.Title.IndexOf(activityFilter.SearchKey, StringComparison.OrdinalIgnoreCase) == -1
                && !activity.Type.Equals(activityFilter.SearchKey, StringComparison.CurrentCultureIgnoreCase)
                && !activity.City.Equals(activityFilter.SearchKey, StringComparison.CurrentCultureIgnoreCase)
                && !activity.Country.Equals(activityFilter.SearchKey, StringComparison.CurrentCultureIgnoreCase)
                )
                return false;
            
            return true;
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
