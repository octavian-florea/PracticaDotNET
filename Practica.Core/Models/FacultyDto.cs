using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Practica.Core
{
    public class FacultyDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string University { get; set; }
    }
}
