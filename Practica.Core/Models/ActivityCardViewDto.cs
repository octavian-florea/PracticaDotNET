using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace Practica.Core
{
    public class ActivityCardViewDto
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Title { get; set; }

        public string CompanyName { get; set; }

        public byte[] CompanyLogo { get; set; }

        public string CompanyLogoExtension { get; set; }

        public string Description { get; set; }

        public string City { get; set; }

    }
}
