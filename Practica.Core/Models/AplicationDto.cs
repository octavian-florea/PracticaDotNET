using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace Practica.Core
{
    public class AplicationDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int ActivityId { get; set; }

        public int FacultyId { get; set; }

        public string Specialization { get; set; }

        public int StudyYear { get; set; }

        public string StudentMessage { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedStateDate { get; set; }
    }
}
