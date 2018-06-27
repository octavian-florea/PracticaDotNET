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
    [Authorize(Roles = "Teacher")]
    [Produces("application/json")]
    [Route("api/statistics")]
    [ValidateModel]
    public class StatisticsController : Controller
    {

        private IStatisticsRepository _statisticsRepository;
        private ILogger<StatisticsController> _logger;

        public StatisticsController(
           IStatisticsRepository statisticsRepository,
           ILogger<StatisticsController> logger)
        {
            _statisticsRepository = statisticsRepository;
            _logger = logger;
        }

        [HttpGet("students")]
        public IActionResult GetNrStudents()
        {
            try
            {
                return Ok(_statisticsRepository.GetNumberOfStudents());
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }
        [HttpGet("companies")]
        public IActionResult GetNrCompanies()
        {
            try
            {
                return Ok(_statisticsRepository.GetNumberOfCompanies());
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }
        [HttpGet("activities/{type}")]
        public IActionResult GetNrActiveActivities(string type)
        {
            try
            {
                return Ok(_statisticsRepository.GetNumberOfActiveActivities(type));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception was thrown: ", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }
    }
}