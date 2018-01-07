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
        private ILogger<ActivityController> _logger;
        private UserManager<PracticaUser> _userManager;

        public AplicationController(
            IAplicationRepository aplicationRepository,
            IActivityRepository activityRepository,
            ILogger<ActivityController> logger,
            UserManager<PracticaUser> userManager)
        {
            _aplicationRepository = aplicationRepository;
            _activityRepository = activityRepository;
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
        public IActionResult GetAplicationsByUser()
        {
            try
            {
                var aplications= _aplicationRepository.GetAllByUser(_userManager.GetUserId(User));

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
        public IActionResult CreateAplication([FromBody]AplicationCreateDto aplicationCreateDto)
        {
            try
            {
                // Validation
                if (aplicationCreateDto == null)
                {
                    return BadRequest();
                }
                var activityEntity = _activityRepository.Get(aplicationCreateDto.ActivityId);
                if(activityEntity == null)
                {
                    return BadRequest($"Activity with id {aplicationCreateDto.ActivityId} was not found");
                }

                // Create the new object
                var aplicationEntity = Mapper.Map<Aplication>(aplicationCreateDto);
                aplicationEntity.UserId= _userManager.GetUserId(User);

                _aplicationRepository.Add(aplicationEntity);

                if (!_aplicationRepository.Save())
                {
                    return StatusCode(500, "A problem happend while handeling your request.");
                }

                var aplicationDtoToReturn = Mapper.Map<AplicationDto>(aplicationEntity);

                return CreatedAtRoute("GetAplication", new { id = aplicationDtoToReturn.Id }, aplicationDtoToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }
    }
}
