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
        private UserManager<PracticaUser> _userManager;
        private ILogger<AuthController> _logger;

        public AuthController(PracticaContext context, 
            SignInManager<PracticaUser> signInManager, 
            UserManager<PracticaUser> userManager, 
            ILogger<AuthController> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _userManager = userManager;
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

                var result = await _signInManager.PasswordSignInAsync(credentialDto.UserName, credentialDto.Password, false, false);
                if (result.Succeeded)
                {
                    return Ok();
                }


            }catch(Exception ex)
            {
                _logger.LogError($"Exception thorwn while loggin in: {ex}");
            }

            return BadRequest("Failed to login");
        }

        [HttpPost("api/auth/register")]
        [ValidateModel]
        public async Task<IActionResult> Register([FromBody]RegisterDto registerDto)
        {
            try
            {
                if (registerDto == null)
                {
                    return BadRequest();
                }

                var user = new PracticaUser
                {
                    UserName = registerDto.UserName
                };

                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return Ok();
                }


            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thorwn while registering : {ex}");
            }

            return BadRequest("Failed to register");
        }

    }
}
