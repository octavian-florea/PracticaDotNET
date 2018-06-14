using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace Practica.Core
{
    public class CompanyProfileViewDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Logo { get; set; }

        public string LogoExtension { get; set; }

        public string Adress { get; set; }

        public string Website { get; set; }
    }
}
