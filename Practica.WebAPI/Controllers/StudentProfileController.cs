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
    [Authorize(Roles = "Student")]
    [Produces("application/json")]
    [Route("api/student/profile")]
    [ValidateModel]
    public class StudentProfileController : Controller
    {
        private IStudentProfileRepository _studentProfileRepository;
        private ILogger<StudentProfileController> _logger;
        private UserManager<PracticaUser> _userManager;

        public StudentProfileController(
            IStudentProfileRepository studentProfileRepository,
            ILogger<StudentProfileController> logger,
            UserManager<PracticaUser> userManager)
        {
            _studentProfileRepository = studentProfileRepository;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet(Name = "GetStudentProfile")]
        public IActionResult GetStudentProfile()
        {
            try
            {
                var studentProfileDto = new StudentProfileDto()
                {
                    Name = "",
                    Description = "",
                    FacultyId = "",
                    FacultyName = "",
                    Specialization = "",
                    StudyYear = 1,
                    Email = "",
                    Telephone = "",
                    City = ""
                };
                var studentProfile = _studentProfileRepository.Get(User.FindFirst(JwtRegisteredClaimNames.Sid).Value);
                if (studentProfile != null)
                {
                    studentProfileDto = Mapper.Map<StudentProfileDto>(studentProfile);
                }
     
                return Ok(studentProfileDto);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }

        [HttpPut]
        public IActionResult CreateUpdateStudentProfile([FromBody]StudentProfileDto studentProfileDto)
        {
            try
            {
                var profileid = User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
                // Validation
                if (studentProfileDto == null)
                {
                    return BadRequest();
                }

                // Create the new object
                var flagCreate = false;
                var studentProfile = _studentProfileRepository.Get(profileid);
                if (studentProfile == null)
                {
                    flagCreate = true;
                    studentProfile = new StudentProfile
                    {
                        UserId = profileid
                    };
                }
                Mapper.Map(studentProfileDto, studentProfile);

                // Save to DB
                if (flagCreate)
                {
                    _studentProfileRepository.Add(studentProfile);
                }
                if (!_studentProfileRepository.Save())
                {
                    return StatusCode(500, "A problem happend while handeling your request.");
                }

                var studentProfileDtoToReturn = Mapper.Map<StudentProfileDto>(studentProfile);

                return CreatedAtRoute("GetStudentProfile", studentProfileDtoToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }

        [HttpDelete]
        public IActionResult DeleteStudentProfile()
        {
            try
            {
                var profileid = User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
                var studentProfile = _studentProfileRepository.Get(profileid);
                if (studentProfile == null)
                {
                    _logger.LogInformation($"student profile with id {profileid} was not found");
                    return NotFound();
                }

                _studentProfileRepository.Remove(studentProfile);

                if (!_studentProfileRepository.Save())
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
