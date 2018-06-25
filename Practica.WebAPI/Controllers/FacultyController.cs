using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Practica.Core;

namespace Practica.WebAPI
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/faculties")]
    [ValidateModel]
    public class FacultyController : Controller
    {

        private IFacultyRepository _facultyRepository;
        private ILogger<FacultyController> _logger;

        public FacultyController(
           IFacultyRepository facultyRepository,
           ILogger<FacultyController> logger)
        {
            _facultyRepository = facultyRepository;
            _logger = logger;
        }

        [HttpGet("{quary}", Name = "GetFaculties")]
        public IActionResult GetFaculty(string quary)
        {
            try
            {
                var faculties = _facultyRepository.GetAllByQueryName(quary);

                List<FacultyDto> facultiesDto = new List<FacultyDto>();
                foreach (var faculty in faculties)
                {
                    FacultyDto activityCardDto = new FacultyDto()
                    {
                        Id = faculty.Id,
                        Name = faculty.Name,
                        University = faculty.University?.Name
                    };
                    facultiesDto.Add(activityCardDto);
                }

                return Ok(facultiesDto);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }
    }
}