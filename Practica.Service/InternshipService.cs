using Practica.Core;
using System;
using System.Collections.Generic;

namespace Practica.Service
{
    public class InternshipService: IInternshipService
    {
        private readonly IIntershipRepository _intershipRepository;

        public InternshipService(IIntershipRepository intershipRepository)
        {
            _intershipRepository = intershipRepository;
        }

        public Internship GetById(int id)
        {
            return _intershipRepository.Get(id);
        }

        public IEnumerable<Internship> GetAll()
        {
            return null;
        }

        public Internship Create(Internship internship)
        {
           // if (String.IsNullOrEmpty(internship.Id))
            //{
            //    internship.Id = Guid.NewGuid().ToString();
           // }
            _intershipRepository.Add(internship);
            return internship;
        }

        public bool Update(Internship internship)
        {
            _intershipRepository.Add(internship);
            return true;
        }

        public bool Delete(int id)
        {
            return _intershipRepository.Remove(id);
        }
    }
}
