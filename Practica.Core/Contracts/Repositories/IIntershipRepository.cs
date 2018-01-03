using System;
using System.Collections.Generic;
using System.Text;

namespace Practica.Core
{
    public interface IIntershipRepository: IRepository
    {
        Internship Get(int id);
        void Add(Internship internship);
        bool Remove(int id);
    }
}
