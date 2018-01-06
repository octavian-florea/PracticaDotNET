using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practica.Service;
using Practica.Core;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Practica.Data;

namespace Practica.WebAPI
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/internships")]
    [ValidateModel]
    public class IntershipController : Controller
    {
        private ILogger<IntershipController> _logger;
        private IInternshipService _intershipService;
        private PracticaContext _context;

        public IntershipController(ILogger<IntershipController> logger, IInternshipService intershipService, PracticaContext context)
        {
            _logger = logger;
            _intershipService = intershipService;
            _context = context;
        }

        [HttpGet("testdatabase")]
        public IActionResult TestDatebase()
        {
            return Ok();
        }

        [HttpGet("{id}", Name = "GetIntershipById")]
        public IActionResult GetIntership(int id)
        {
            try
            {
                Internship internship = _intershipService.GetById(id);
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
            Internship internship = new Internship();
            internship.Title = intershipCreateDto.Title;
            internship.Description = intershipCreateDto.Description;
            internship.StartDate = intershipCreateDto.StartDate;
            internship.EndDate = intershipCreateDto.EndDate;

            internship = _intershipService.Create(internship);

            return CreatedAtRoute("GetIntershipById", new {id = internship.Id}, internship);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateIntership(int id, [FromBody]IntershipUpdateDto intershipUpdateDto)
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

            Internship internship = _intershipService.GetById(id);
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
