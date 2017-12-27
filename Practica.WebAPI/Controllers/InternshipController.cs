using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practica.Service;
using Practica.Data;
using Practica.Core;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Practica.WebAPI
{
    [Produces("application/json")]
    [Route("api/v1/internship")]
    public class IntershipController : Controller
    {
        private ILogger<IntershipController> _logger;

        public IntershipController(ILogger<IntershipController> logger)
        {
            _logger = logger;
        }

        InternshipService _intershipService = new InternshipService(new IntershipRepository());

        [HttpGet("{id}", Name = "GetIntership")]
        public IActionResult GetIntership(string id)
        {
            try
            {
                throw new Exception("aa");
                Internship internship = _intershipService.GetInternshipById(id);
                if (internship == null)
                {
                    _logger.LogInformation($"Intership with id {id} was not found");
                    return NotFound();
                }
                else
                    return Ok(internship);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ",ex);
                return StatusCode(500,"A problem happend while handeling your request.");
            }
        }

        [HttpPost]
        public IActionResult CreateIntership([FromBody]IntershipCreateDto intershipCreateDto)
        {
            // validation
            if(intershipCreateDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // create the new object
            //Internship internship;

            //return CreatedAtRoute("GetIntership", new {id = 5}, internship);
            return CreatedAtRoute("GetIntership", new { id = 5 });

        }

        [HttpPut("{id}")]
        public IActionResult UpdateIntership(string id, [FromBody]IntershipUpdateDto intershipUpdateDto)
        {
            // validation
            if (intershipUpdateDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Internship internship = _intershipService.GetInternshipById(id);
            if (internship == null)
                return NotFound();
            else
            {
                internship.Title = intershipUpdateDto.Title; ;

                return NoContent();
            }
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteIntership(string id)
        {
            return NoContent();
        }
    }
}
