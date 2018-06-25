using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace Practica.Core
{
    public class StudentProfileDto
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string FacultyId { get; set; }

        public string FacultyName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Specialization { get; set; }

        [Required]
        public int StudyYear { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Telephone { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        public Byte[] CV { get; set; }
    }
}
