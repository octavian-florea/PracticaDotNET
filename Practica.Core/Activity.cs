using System;
using System.Data.Common;

namespace Practica.Core
{
    public class Activity
    {
        private int Id { get; set; }
        private string Title { get; set; }
        private string Description { get; set; }
        private DateTime StartDate { get; set; }
        private DateTime EndDate { get; set; }

        public Activity(int id, string title, string description, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
        }

    }
}
