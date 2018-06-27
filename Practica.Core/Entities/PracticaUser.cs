using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Practica.Core
{
    public class PracticaUser: IdentityUser
    {
        public CompanyProfile CompanyProfile { get; set; }
        public StudentProfile StudentProfile { get; set; }
        public ICollection<Activity> Activities { get; set; }
    }
}
