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

        public ActivityDto Activity { get; set; }
    }
}
