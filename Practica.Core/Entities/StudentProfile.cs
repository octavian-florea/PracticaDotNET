﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace Practica.Core
{
    public class StudentProfile
    {
        [Key]
        [Required]
        [ForeignKey("User")]
        [MaxLength(450)]
        public string UserId { get; set; }
        public PracticaUser User { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [Required]
        [ForeignKey("Faculty")]
        [MaxLength(10)]
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }

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

        [Column(TypeName = "image")]
        public Byte[] CV { get; set; }

    }
}
