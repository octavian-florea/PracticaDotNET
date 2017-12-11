using System;
using System.Collections.Generic;
using System.Text;
using Practica.Data;
using System.Collections;
using Practica.Core;

namespace Practica.Service
{
    public class ActivityService
    {
        private readonly IActivityQueryRepository _activityRepository;

        public ActivityService(IActivityQueryRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public List<Activity> Find(ActivityFilter filters)
        {
            return _activityRepository.Find(filters);
        }

        public List<String> GetActivity() {

            ActivityQueryRepository queryRepo = new ActivityQueryRepository();
       

            return queryRepo.getActivity();


        }
        
    }
}
