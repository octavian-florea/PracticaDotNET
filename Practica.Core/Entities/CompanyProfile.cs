using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace Practica.Core
{
    public class CompanyProfile
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

        [Column(TypeName = "image")]
        public byte[] Logo { get; set; }

        [MaxLength(250)]
        public string Adress { get; set; }

        [MaxLength(200)]
        public string Website { get; set; }
    }
}
