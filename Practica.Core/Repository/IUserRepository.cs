using System;
using System.Collections.Generic;
using System.Text;

namespace Practica.Core
{
    public interface IUserRepository
    {
        User Get(string id);
        void Add(User user);
    }
}
