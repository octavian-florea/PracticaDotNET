using System;
using System.Data.Common;

namespace Practica.Core
{
    public class Activity
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Activity(string id, string title, string description, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
        }

    }
}
