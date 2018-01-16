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
    [Authorize]
    [Produces("application/json")]
    [Route("api/universities")]
    [ValidateModel]
    public class UniversityController : Controller
    {

        private IUniversityRepository _universityRepository;
        private ILogger<UniversityController> _logger;

        public UniversityController(
           IUniversityRepository universityRepository,
           ILogger<UniversityController> logger)
        {
            _universityRepository = universityRepository;
            _logger = logger;
        }
    }
}