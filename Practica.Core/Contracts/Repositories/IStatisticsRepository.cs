using System.Collections.Generic;

namespace Practica.Core
{
    public interface IStatisticsRepository
    {
        int GetNumberOfStudents();
        int GetNumberOfCompanies();
        int GetNumberOfActiveActivities(string activityType);
    }
}