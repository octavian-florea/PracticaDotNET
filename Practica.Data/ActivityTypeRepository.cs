using Practica.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Practica.Data
{
    public class ActivityTypeRepository : IActivityTypeRepository
    {
        private PracticaContext _context;

        public ActivityTypeRepository(PracticaContext context)
        {
            _context = context;
        }
        public IEnumerable<ActivityType> GetAll()
        {
            return _context.ActivityTypes.ToList();
        }

        public bool ValidActivityType(string code)
        {
            var activityType = _context.ActivityTypes.Where(c => c.Code.Equals(code)).FirstOrDefault();
            if(activityType == null)
            {
                return false;
            }
            return true;
        }
    }
}
