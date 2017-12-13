using Practica.Core;
using System;

namespace Practica.Service
{
    public class InternshipService
    {
        private readonly IIntershipRepository _intershipRepository;

        public InternshipService(IIntershipRepository intershipRepository)
        {
            _intershipRepository = intershipRepository;
        }

        public Internship GetInternshipById(string id)
        {
            return _intershipRepository.Get(id);
        }
    }
}
