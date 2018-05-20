using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace Practica.Core
{
    public class AplicationUpdateDto
    {
        [Required]
        public byte Status { get; set; }
    }
}
