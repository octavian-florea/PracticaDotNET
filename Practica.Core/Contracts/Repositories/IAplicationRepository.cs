using System;
using System.Collections.Generic;
using System.Text;

namespace Practica.Core
{
    public interface IAplicationRepository
    {
        void Add(Aplication aplication);
        Aplication Get(int id);
        IEnumerable<Aplication> GetAllByActivity(int aplicationid);
        IEnumerable<Aplication> GetAllByActivityAndStudent(string studentid, int aplicationid);
        IEnumerable<Aplication> GetAllByUser(string userid);
        void Remove(Aplication aplication);
        bool Save();
    }
}
