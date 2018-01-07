using System;
using System.Collections.Generic;
using System.Text;

namespace Practica.Core
{
    public interface IAplicationRepository
    {
        void Add(Aplication aplication);
        Aplication Get(int id);
        IEnumerable<Aplication> GetAll();
        IEnumerable<Aplication> GetAllByUser(string userid);
        IEnumerable<Aplication> Find();
        void Remove(Aplication aplication);
        bool Save();
    }
}
