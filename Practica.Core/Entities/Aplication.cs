using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;

namespace Practica.Core
{
    public class Aplication
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [ForeignKey("PracticaUser")]
        [MaxLength(450)]
        public string UserId { get; set; }
        public PracticaUser PracticaUser { get; set; }

        [ForeignKey("Activity")]
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

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

        [Column(TypeName = "text")]
        public string StudentMessage { get; set; }

        [Required]
        public byte Status { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime ModifiedStateDate { get; set; }

    }
}
