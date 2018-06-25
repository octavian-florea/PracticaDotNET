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
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Practica.WebAPI
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/aplications")]
    [ValidateModel]
    public class AplicationController : Controller
    {
        private IAplicationRepository _aplicationRepository;
        private IActivityRepository _activityRepository;
        private IStudentProfileRepository _studentProfileRepository;
        private ILogger<ActivityController> _logger;
        private UserManager<PracticaUser> _userManager;

        public AplicationController(
            IAplicationRepository aplicationRepository,
            IActivityRepository activityRepository,
            IStudentProfileRepository stundetProfileRepository,
            ILogger<ActivityController> logger,
            UserManager<PracticaUser> userManager)
        {
            _aplicationRepository = aplicationRepository;
            _activityRepository = activityRepository;
            _studentProfileRepository = stundetProfileRepository;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet("{id}", Name ="GetAplication")]
        public IActionResult GetAplication(int id)
        {
            try
            {
                var aplicationEntity = _aplicationRepository.Get(id);
                if (aplicationEntity == null)
                {
                    _logger.LogInformation($"Aplication with id {id} was not found");
                    return NotFound();
                }

                var aplicationDto = Mapper.Map<AplicationDto>(aplicationEntity);
                return Ok(aplicationDto);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }

        [HttpGet("user", Name = "GetAplicationsByUser")]
        [Authorize(Roles = "Student")]
        public IActionResult GetAplicationsByUser()
        {
            try
            {
                var aplications= _aplicationRepository.GetAllByUser(User.FindFirst(JwtRegisteredClaimNames.Sid).Value);

                var result = Mapper.Map<IEnumerable<AplicationDto>>(aplications);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }

        [HttpGet("activities/{id}", Name = "GetAplicationsByActivity")]
        [Authorize(Roles = "Company")]
        public IActionResult GetAplicationsByActivity(int id)
        {
            try
            {
                var aplications = _aplicationRepository.GetAllByActivity(id);

                var result = Mapper.Map<IEnumerable<AplicationDto>>(aplications);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }

        [HttpGet("activities/{id}/user", Name = "GetAplicationsByActivityByUser")]
        public IActionResult GetAplicationsByActivityByUser(int id)
        {
            try
            {
                var studentid = User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
                var aplications = _aplicationRepository.GetAllByActivityAndStudent(studentid, id);

                var result = Mapper.Map<IEnumerable<AplicationDto>>(aplications);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public IActionResult CreateAplication([FromBody]AplicationCreateDto aplicationCreateDto)
        {
            try
            {
                // Validation
                if (aplicationCreateDto == null)
                {
                    return BadRequest();
                }
                var activity = _activityRepository.Get(aplicationCreateDto.ActivityId);
                if(activity == null)
                {
                    return BadRequest($"Activity with id {aplicationCreateDto.ActivityId} was not found");
                }


                // Get student data
                var studentid = User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
                var studentProfile = _studentProfileRepository.Get(studentid);
                if (studentProfile == null)
                {
                    return BadRequest($"Student profile was not found");
                }

                // Stundet can apply only once
                if (_aplicationRepository.GetAllByActivityAndStudent(studentid, aplicationCreateDto.ActivityId).Count() > 0)
                {
                    return BadRequest($"You have already applyed for this activity");
                }

                // Create the new object
                var aplication = Mapper.Map<Aplication>(aplicationCreateDto);
                aplication.UserId = studentid;
                aplication.FacultyId = studentProfile.FacultyId;
                aplication.Specialization = studentProfile.Specialization;
                aplication.StudyYear = studentProfile.StudyYear;
                aplication.Status = 0;
                aplication.CreatedDate = DateTime.Now;
                aplication.ModifiedStateDate = DateTime.Now;

                _aplicationRepository.Add(aplication);

                if (!_aplicationRepository.Save())
                {
                    return StatusCode(500, "A problem happend while handeling your request.");
                }

                var aplicationDtoToReturn = Mapper.Map<AplicationDto>(aplication);

                return CreatedAtRoute("GetAplication", new { id = aplicationDtoToReturn.Id }, aplicationDtoToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Company")]
        public IActionResult UpdateAplication(int id, [FromBody]AplicationUpdateDto aplicationUpdateDto)
        {
            try
            {
                if (aplicationUpdateDto == null)
                {
                    return BadRequest();
                }

                var aplication = _aplicationRepository.Get(id);
                if (aplication == null)
                {
                    _logger.LogInformation($"Acitvity with id {id} was not found");
                    return NotFound();
                }

                Mapper.Map(aplicationUpdateDto, aplication);
                aplication.ModifiedStateDate = DateTime.Now;

                if (!_aplicationRepository.Save())
                {
                    return StatusCode(500, "A problem happend while handeling your request.");
                }

                var aplicationDtoToReturn = Mapper.Map<AplicationDto>(aplication);

                return CreatedAtRoute("GetActivity", new { id = aplicationDtoToReturn.Id }, aplicationDtoToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Student")]
        public IActionResult DeleteAplication(int id)
        {
            try
            {
                var aplication = _aplicationRepository.Get(id);
                if (aplication == null)
                {
                    _logger.LogInformation($"Aplication with id {id} was not found");
                    return NotFound();
                }

                _aplicationRepository.Remove(aplication);

                if (!_aplicationRepository.Save())
                {
                    return StatusCode(500, "A problem happend while handeling your request.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }
    }
}
