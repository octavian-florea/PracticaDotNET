using System;
using System.Collections.Generic;
using System.Text;

namespace Practica.Core
{
    public class Internship : Activity
    {
        public Internship(int id, string title, string description, DateTime startDate, DateTime endDate) : base(id, title, description, startDate, endDate)
        {

        }
    }
}
