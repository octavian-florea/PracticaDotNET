using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace Practica.Core
{
    public class ActivityCreateDto
    {
        [Required]
        [MaxLength(10)]
        public string Type { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public System.Nullable<DateTime> StartDate { get; set; }

        [Required]
        [DateGreaterThan("StartDate",ErrorMessage = "End date needs to be greater than start date")]
        public System.Nullable<DateTime> EndDate { get; set; }

        public System.Nullable<DateTime> PublishDate { get; set; }

        public System.Nullable<DateTime> ExpirationDate { get; set; }

        [Required]
        public int Seats { get; set; }

        [Required]
        [MaxLength(50)]
        public string Country { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }
        
    }
}
