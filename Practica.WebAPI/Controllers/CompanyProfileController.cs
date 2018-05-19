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
    [Authorize(Roles = "Company")]
    [Produces("application/json")]
    [Route("api/company/profile")]
    [ValidateModel]
    public class CompanyProfileController : Controller
    {
        private ICompanyProfileRepository _companyProfileRepository;
        private ILogger<ActivityController> _logger;
        private UserManager<PracticaUser> _userManager;

        public CompanyProfileController(
            ICompanyProfileRepository companyProfileRepository,
            ILogger<ActivityController> logger,
            UserManager<PracticaUser> userManager)
        {
            _companyProfileRepository = companyProfileRepository;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet(Name = "GetCompanyProfile")]
        public IActionResult GetCompanyProfile()
        {
            try
            {
                var companyProfile = _companyProfileRepository.Get(User.FindFirst(JwtRegisteredClaimNames.Sid).Value);
                if (companyProfile == null)
                {
                    _logger.LogInformation($"Company profile was not found");
                    return NotFound();
                }

                var companyProfileDto = Mapper.Map<CompanyProfileDto>(companyProfile);
                return Ok(companyProfileDto);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }

        [HttpPost]
        public IActionResult CreateActivity([FromBody]CompanyProfileDto companyProfileDto)
        {
            try
            {
                var profileid = User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
                // Validation
                if (companyProfileDto == null)
                {
                    return BadRequest();
                }
                var companyProfileInDB = _companyProfileRepository.Get(profileid);
                if (companyProfileInDB != null)
                {
                    _logger.LogInformation($"Profila {profileid} allready exists");
                    return BadRequest("Profila allready exists");
                }

                // Create the new object
                var companyProfile = Mapper.Map<CompanyProfile>(companyProfileDto);
                companyProfile.UserId = profileid;

                _companyProfileRepository.Add(companyProfile);

                if (!_companyProfileRepository.Save())
                {
                    return StatusCode(500, "A problem happend while handeling your request.");
                }

                var companyProfileDtoToReturn = Mapper.Map<CompanyProfileDto>(companyProfile);

                return CreatedAtRoute("GetCompanyProfile", companyProfileDtoToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }

        [HttpPut]
        public IActionResult UpdateActivity([FromBody]CompanyProfileDto companyProfileDto)
        {
            try
            {
                var profileid = User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
                if (companyProfileDto == null)
                {
                    return BadRequest();
                }
                var companyProfile = _companyProfileRepository.Get(profileid);
                if (companyProfile == null)
                {
                    _logger.LogInformation($"Company profile with id {profileid} was not found");
                    return NotFound();
                }

                Mapper.Map(companyProfileDto, companyProfile);

                if (!_companyProfileRepository.Save())
                {
                    return StatusCode(500, "A problem happend while handeling your request.");
                }

                var companyProfileDtoToReturn = Mapper.Map<CompanyProfileDto>(companyProfile);

                return CreatedAtRoute("GetCompanyProfile", companyProfileDtoToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }

        [HttpDelete]
        public IActionResult DeleteActivity(int id)
        {
            try
            {
                var profileid = User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
                var companyProfile = _companyProfileRepository.Get(profileid);
                if (companyProfile == null)
                {
                    _logger.LogInformation($"company profile with id {profileid} was not found");
                    return NotFound();
                }

                _companyProfileRepository.Remove(companyProfile);

                if (!_companyProfileRepository.Save())
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
