using System;
using System.Collections.Generic;
using System.Text;

namespace Practica.Core
{
    public interface IActivityTypeRepository
    {
        IEnumerable<ActivityType> GetAll();
        bool ValidActivityType(string code);
    }
}
