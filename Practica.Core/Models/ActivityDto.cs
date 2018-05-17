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

        public string Type { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public System.Nullable<DateTime> CreatedDate { get; set; }

        public System.Nullable<DateTime> StartDate { get; set; }

        public System.Nullable<DateTime> EndDate { get; set; }

        public System.Nullable<DateTime> PublishDate { get; set; }

        public System.Nullable<DateTime> ExpirationDate { get; set; }

        public int Seats { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public ICollection<AplicationDto> Aplications { get; set; }
    }
}
