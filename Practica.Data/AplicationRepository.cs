using Microsoft.EntityFrameworkCore;
using Practica.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Practica.Data
{
    public class AplicationRepository : IAplicationRepository
    {
        private PracticaContext _context;

        public AplicationRepository(PracticaContext context)
        {
            _context = context;
        }

        public void Add(Aplication aplication)
        {
            _context.Aplications.Add(aplication);
        }

        public Aplication Get(int id)
        {
            return _context.Aplications.Where(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<Aplication> GetAllByActivity(int activityId)
        {
            return _context.Aplications.Include(c => c.Activity)
               .Where(c => c.ActivityId == activityId).ToList();
        }

        public IEnumerable<Aplication> GetAllByActivityAndStudent(string studentId, int activityId)
        {
            return _context.Aplications
               .Where(c => c.ActivityId == activityId && c.UserId == studentId).ToList();
        }

        public IEnumerable<Aplication> GetAllByUser(string userid)
        {
            return _context.Aplications.Include(c => c.Activity )
                .Where(c => c.UserId == userid).ToList();
        }

        public void Remove(Aplication aplication)
        {
            _context.Aplications.Remove(aplication);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
