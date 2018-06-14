using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace Practica.Core
{
    public class CompanyProfileDto
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        public string Description { get; set; }

        public IFormFile Logo { get; set; }

        [MaxLength(250)]
        public string Adress { get; set; }

        [MaxLength(200)]
        public string Website { get; set; }
    }
}
