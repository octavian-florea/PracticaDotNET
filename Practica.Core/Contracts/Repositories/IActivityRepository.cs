using System.Collections.Generic;

namespace Practica.Core
{
    public interface IActivityRepository
    {
        void Add(Activity activity);
        Activity Get(int id);
        IEnumerable<Activity> GetAll();
        IEnumerable<Activity> Find(ActivityFilter activityFilter);
        void Remove(Activity activity);
        bool Save();
    }
}