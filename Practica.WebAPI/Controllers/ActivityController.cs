using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practica.Service;

namespace Practica.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Activity")]
    public class ActivityController : Controller
    {
        // GET: api/Activity
        [HttpGet]
        public IEnumerable<string> Get()
        {
            Activity activity = new Activity();
            return activity.GetActivity();
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
