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

    }
}
