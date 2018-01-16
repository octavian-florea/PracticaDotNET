using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace Practica.Core
{
    public class Faculty
    {
        [Key]
        [Required]
        [MaxLength(10)]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [ForeignKey("University")]
        public int UniversityId { get; set; }
        public University University { get; set; }
    }
}
