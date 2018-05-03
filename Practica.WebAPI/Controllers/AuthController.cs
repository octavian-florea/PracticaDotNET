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
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Practica.WebAPI.Controllers
{
    [Produces("application/json")]

    public class AuthController : Controller
    {
        private PracticaContext _context;
        private SignInManager<PracticaUser> _signInManager;
        private UserManager<PracticaUser> _userManager;
        private IPasswordHasher<PracticaUser> _hasher;
        private ILogger<AuthController> _logger;
        private IConfiguration _config;

        public AuthController(PracticaContext context, 
            SignInManager<PracticaUser> signInManager, 
            UserManager<PracticaUser> userManager, 
            IPasswordHasher<PracticaUser> hasher,
            ILogger<AuthController> logger,
            IConfiguration config)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _hasher = hasher;
            _config = config;
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

        [HttpPost("api/auth/token")]
        [ValidateModel]
        public async Task<IActionResult> CreateToken([FromBody]CredentialDto credentialDto)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(credentialDto.UserName);
                if(user != null)
                {
                    if(_hasher.VerifyHashedPassword(user, user.PasswordHash, credentialDto.Password) == PasswordVerificationResult.Success)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            issuer: _config["Token:Issuer"],
                            audience: _config["Token:Audience"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(15),
                            signingCredentials: creds
                            );

                        return Ok(new
                        {
                            toekn = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                    }
                }
            }catch(Exception ex)
            {
                _logger.LogError($"Exception thrown while creating JWT : {ex}");
            }
            return BadRequest("Failed to generate token");
        }
    }
}
