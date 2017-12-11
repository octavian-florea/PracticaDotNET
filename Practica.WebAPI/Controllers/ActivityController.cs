using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practica.Service;
using Practica.Data;
using Practica.Core;
using System.Collections.Generic;

namespace Practica.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Activity")]
    public class ActivityController : Controller
    {

        ActivityService _activityService = new ActivityService(new ActivityQueryRepository());

        // GET: api/Activity
        [HttpGet]
        //public IEnumerable<string> Get()
        public List<Activity> Get(string title)
        {
            ActivityFilter activityFilter = new ActivityFilter(title);
            return _activityService.Find(activityFilter);
        }

        // GET: api/Activity/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Activity
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Activity/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
