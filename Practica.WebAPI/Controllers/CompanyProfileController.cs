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
using System.IO;

namespace Practica.WebAPI
{
    [Authorize(Roles = "Company")]
    [Produces("application/json")]
    [Route("api/company/profile")]
    [ValidateModel]
    public class CompanyProfileController : Controller
    {
        private ICompanyProfileRepository _companyProfileRepository;
        private ILogger<CompanyProfileController> _logger;
        private UserManager<PracticaUser> _userManager;

        public CompanyProfileController(
            ICompanyProfileRepository companyProfileRepository,
            ILogger<CompanyProfileController> logger,
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
                    companyProfile = new CompanyProfile() {
                        Name = "",
                        Description = "",
                        Adress = "",
                        Website = ""
                    };
                }

                var companyProfileDto = Mapper.Map<CompanyProfileViewDto>(companyProfile);
                return Ok(companyProfileDto);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }

        [HttpPut]
        public IActionResult CreateUpdateCompanyProfile([FromForm]CompanyProfileDto companyProfileDto)
        {
            try
            {
                var profileid = User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
                // Validation
                if (companyProfileDto == null)
                {
                    return BadRequest();
                }

                // Validate image
                int MaxContentLength = 1024 * 1024 * 5; //Size = 5 MB  
                IList<string> AllowedFileExtensions = new List<string> { "jpg", "gif", "png" };
                var extension = "";
                bool flagProcessLogo = companyProfileDto.Logo != null;

                if (flagProcessLogo)
                {
                    var ext = companyProfileDto.Logo.FileName.Substring(companyProfileDto.Logo.FileName.LastIndexOf('.')+1);
                    extension = ext.ToLower();
                    if (!AllowedFileExtensions.Contains(extension))
                    {
                        var message = string.Format("Please Upload image of type .jpg,.gif,.png.");
                        _logger.LogInformation(message);
                        return BadRequest(message);
                    }
                    else if (companyProfileDto.Logo.Length > MaxContentLength)
                    {
                        var message = string.Format("Please Upload a file upto 5 mb.");
                        _logger.LogInformation(message);
                        return BadRequest(message);
                    }
                }

                // Create/update 
                var flagCreate = false;
                var companyProfile = _companyProfileRepository.Get(profileid);
                if (companyProfile == null)
                {
                    flagCreate = true;
                    companyProfile = new CompanyProfile
                    {
                        UserId = profileid
                    };
                }
                companyProfile.Name = companyProfileDto.Name;
                companyProfile.Description = companyProfileDto.Description;
                companyProfile.Website = companyProfileDto.Website;
                companyProfile.Adress = companyProfileDto.Adress;

                if (flagProcessLogo)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        companyProfileDto.Logo.CopyTo(memoryStream);
                        companyProfile.Logo = memoryStream.ToArray();
                    }
                    companyProfile.LogoExtension = extension;
                }

                // Save to DB
                if (flagCreate)
                {
                    _companyProfileRepository.Add(companyProfile);     
                }          
                if (!_companyProfileRepository.Save())
                {
                    return StatusCode(500, "A problem happend while handeling your request.");
                }     

                var companyProfileDtoToReturn = Mapper.Map<CompanyProfileViewDto>(companyProfile);

                return CreatedAtRoute("GetCompanyProfile", companyProfileDtoToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }

        [HttpDelete]
        public IActionResult DeleteCompanyProfile()
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
