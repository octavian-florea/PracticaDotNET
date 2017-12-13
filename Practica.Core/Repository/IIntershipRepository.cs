using System;
using System.Collections.Generic;
using System.Text;

namespace Practica.Core
{
    public interface IIntershipRepository: IRepository
    {
        Internship Get(string id);
    }
}
