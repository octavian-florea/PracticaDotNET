using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace Practica.Core
{
    public class ActivityDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime AplicationEndDate { get; set; }

        public string Type { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string City { get; set; }

        public string Addres { get; set; }

        public int Seats { get; set; }

        public ICollection<AplicationDto> Aplications { get; set; }
    }
}
