using System;
using System.Collections.Generic;
using System.Text;
using Practica.Data;

namespace Practica.Service
{
    public class Activity
    {
        public Activity()
        {
        }

        public List<String> GetActivity() {

            QueryActivityRepository queryRepo = new QueryActivityRepository();
       

            return queryRepo.getActivity();


        }
        
    }
}
