using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace Practica.Core
{
    public class Activity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("PracticaUser")]
        [MaxLength(450)]
        public string UserId { get; set; }
        public PracticaUser PracticaUser { get; set; }

        [Required]
        [ForeignKey("ActivityType")]
        [MaxLength(10)]
        public string Type { get; set; }
        public ActivityType ActivityType { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public System.Nullable<DateTime> StartDate { get; set; }

        [Required]
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

        public virtual ICollection<Aplication> Aplications { get; set; }

    }
}
