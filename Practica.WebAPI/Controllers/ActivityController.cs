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
    [Route("api/activities")]
    [ValidateModel]
    public class ActivityController : Controller
    {
        private IActivityRepository _activityRepository;
        private IActivityTypeRepository _activityTypeRepository;
        private ILogger<ActivityController> _logger;
        private UserManager<PracticaUser> _userManager;

        public ActivityController(
            IActivityRepository activityRepository,
            IActivityTypeRepository activityTypeRepository,
            ILogger<ActivityController> logger,
            UserManager<PracticaUser> userManager)
        {
            _activityRepository = activityRepository;
            _activityTypeRepository = activityTypeRepository;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Student")]
        public IActionResult GetActivities()
        {
            try
            {
                var activities = _activityRepository.GetAll();

                var result = Mapper.Map<IEnumerable<ActivityDto>>(activities);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }

        [HttpGet("{id}", Name ="GetActivity")]
        public IActionResult GetActivity(int id)
        {
            try
            {
                var activityEntity = _activityRepository.Get(id);
                if (activityEntity == null)
                {
                    _logger.LogInformation($"Activity with id {id} was not found");
                    return NotFound();
                }

                var activityDto = Mapper.Map<ActivityDto>(activityEntity);
                return Ok(activityDto);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }

        [HttpGet("user")]
        public IActionResult GetActivityByUser()
        {
            try
            {
                var activities = _activityRepository.GetAllByUser(User.FindFirst(JwtRegisteredClaimNames.Sid).Value);

                var result = Mapper.Map<IEnumerable<ActivityDto>>(activities);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Company")]
        public IActionResult CreateActivity([FromBody]ActivityCreateDto activityCreateDto)
        {
            try
            {
                // Validation
                if (activityCreateDto == null)
                {
                    return BadRequest();
                }
                if (!_activityTypeRepository.ValidActivityType(activityCreateDto.Type))
                {
                    return BadRequest("Type is not valid");
                };
                if (activityCreateDto.ExpirationDate!= null && activityCreateDto.ExpirationDate <= DateTime.Now)
                {
                    return BadRequest("ExpirationDate needs to be in the future");
                }
                if (activityCreateDto.PublishDate != null && activityCreateDto.PublishDate <= DateTime.Now)
                {
                    return BadRequest("PublishDate needs to be in the present or in the future");
                }

                // Create the new object
                var activityEntity = Mapper.Map<Activity>(activityCreateDto);
                activityEntity.UserId= User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
                activityEntity.CreatedDate = DateTime.Now;
                if (activityEntity.PublishDate == null)
                {
                    activityEntity.PublishDate = DateTime.Parse("9999-12-31");
                }

                _activityRepository.Add(activityEntity);

                if (!_activityRepository.Save())
                {
                    return StatusCode(500, "A problem happend while handeling your request.");
                }

                var activityDtoToReturn = Mapper.Map<ActivityDto>(activityEntity);

                return CreatedAtRoute("GetActivity", new { id = activityDtoToReturn.Id }, activityDtoToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateActivity(int id, [FromBody]ActivityUpdateDto activityUpdateDto)
        {
            try
            {
                if (activityUpdateDto == null)
                {
                    return BadRequest();
                }
                if (activityUpdateDto.ExpirationDate != null && activityUpdateDto.ExpirationDate <= DateTime.Now)
                {
                    return BadRequest("ExpirationDate needs to be in the future");
                }
                if (activityUpdateDto.PublishDate != null && activityUpdateDto.PublishDate <= DateTime.Now)
                {
                    return BadRequest("PublishDate needs to be in the present or in the future");
                }
                if (activityUpdateDto.PublishDate == null)
                {
                    activityUpdateDto.PublishDate = DateTime.Parse("9999-12-31");
                }


                var activity = _activityRepository.Get(id);
                if (activity == null)
                {
                    _logger.LogInformation($"Acitvity with id {id} was not found");
                    return NotFound();
                }

                Mapper.Map(activityUpdateDto, activity);

                if (!_activityRepository.Save())
                {
                    return StatusCode(500, "A problem happend while handeling your request.");
                }

                var activityDtoToReturn = Mapper.Map<ActivityDto>(activity);

                return CreatedAtRoute("GetActivity", new { id = activityDtoToReturn.Id }, activityDtoToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult PatchActivity(int id,[FromBody] JsonPatchDocument<ActivityUpdateDto> patchDoc)
        {
            try
            {
                if (patchDoc == null)
                {
                    return BadRequest();
                }

                var activityEntety = _activityRepository.Get(id);
                if (activityEntety == null)
                {
                    _logger.LogInformation($"Acitvity with id {id} was not found");
                    return NotFound();
                }

                var activityUpdateDto = Mapper.Map<ActivityUpdateDto>(activityEntety);

                patchDoc.ApplyTo(activityUpdateDto, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                TryValidateModel(activityUpdateDto);

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                Mapper.Map(activityUpdateDto, activityEntety);

                if (!_activityRepository.Save())
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

        [HttpDelete("{id}")]
        public IActionResult DeleteActivity(int id)
        {
            try
            {
                var activityEntety = _activityRepository.Get(id);
                if (activityEntety == null)
                {
                    _logger.LogInformation($"Acitvity with id {id} was not found");
                    return NotFound();
                }

                _activityRepository.Remove(activityEntety);

                if (!_activityRepository.Save())
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
