using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Practica.Core;
using Microsoft.Extensions.Logging;
using Practica.Data;

namespace Practica.WebAPI.Controllers
{
    [Produces("application/json")]

    public class AuthController : Controller
    {
        private PracticaContext _context;
        private SignInManager<PracticaUser> _signInManager;
        private ILogger<AuthController> _logger;

        public AuthController(PracticaContext context, SignInManager<PracticaUser> signInManager, ILogger<AuthController> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [HttpPost("api/auth/login")]
        [ValidateModel]
        public async Task<IActionResult> Login([FromBody]CredentialDto credentialDto)
        {
            try
            {
                if (credentialDto == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _signInManager.PasswordSignInAsync(credentialDto.UserName, credentialDto.Password, false, false);
                if (result.Succeeded)
                {
                    return Ok();
                }


            }catch(Exception ex)
            {
                _logger.LogError($"Exception thorwn while loggin in: {ex}");
            }

            return BadRequest();
        }

    }
}
