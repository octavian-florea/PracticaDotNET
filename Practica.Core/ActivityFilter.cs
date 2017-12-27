using System;
using System.Collections.Generic;
using System.Text;

namespace Practica.Core
{
    public class ActivityFilter: IActivityFilter
    {
        public string Title { get; set; }

        public ActivityFilter()
        {
            Title = "";
        }

        public ActivityFilter(string title)
        {
            Title = title;
        }

    }
}
