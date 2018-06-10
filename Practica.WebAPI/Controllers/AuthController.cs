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
        private RoleManager<IdentityRole> _roleMgr;
        private IPasswordHasher<PracticaUser> _hasher;
        private ILogger<AuthController> _logger;
        private IConfiguration _config;

        public AuthController(PracticaContext context, 
            SignInManager<PracticaUser> signInManager, 
            UserManager<PracticaUser> userManager,
            RoleManager<IdentityRole> roleMgr,
            IPasswordHasher<PracticaUser> hasher,
            ILogger<AuthController> logger,
            IConfiguration config)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleMgr = roleMgr;
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

                var user = await _userManager.FindByEmailAsync(credentialDto.Email);
                if (user != null)
                {
                    if (_hasher.VerifyHashedPassword(user, user.PasswordHash, credentialDto.Password) == PasswordVerificationResult.Success)
                    {
                        return Ok(await CreateToken(user));
                    }
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
                // validation
                if (registerDto == null)
                {
                    return BadRequest();
                }
                if(!await _roleMgr.RoleExistsAsync(registerDto.Role))
                {
                    String[] acceptedRoles = new String[_roleMgr.Roles.Count()];
                    int counter = 0;
                    foreach (IdentityRole role in _roleMgr.Roles.Where(role => !role.ToString().Equals("Admin")))
                    {
                        acceptedRoles[counter] = role.ToString();
                        counter++;
                    }
                        return BadRequest(String.Join(",", acceptedRoles));
                }
                if (registerDto.Role.Equals("Admin", StringComparison.CurrentCultureIgnoreCase))
                {
                    return BadRequest();
                }
                if (await _userManager.FindByEmailAsync(registerDto.Email) != null){
                    return BadRequest("Email already exists");
                }


                // register user
                var user = new PracticaUser
                {
                    UserName = registerDto.Email,
                    Email = registerDto.Email
                };

                var result = await _userManager.CreateAsync(user, registerDto.Password);
                var roleResult = await _userManager.AddToRoleAsync(user, registerDto.Role);
                if (result.Succeeded && roleResult.Succeeded)
                {
                    return Ok(await CreateToken(user));
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thorwn while registering : {ex}");
            }

            return BadRequest("Failed to register");
        }

        [HttpGet("api/auth/emailexists/{email}")]
        [ValidateModel]
        public async Task<IActionResult> EmailExists(string email)
        {
            try
            {
                // validation
                if (email == null)
                {
                    return BadRequest();
                }

                if (await _userManager.FindByEmailAsync(email) != null)
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thorwn while registering : {ex}");
            }

            return BadRequest("Failed to get user");
        }

        public async Task<Object> CreateToken(PracticaUser user)
        {
            try
            {
                if(user != null)
                {  
                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Sid, user.Id),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    // add roles to claims for autorization
                    var userRoles = await _userManager.GetRolesAsync(user);
                    foreach (var userRole in userRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: _config["Token:Issuer"],
                        audience: _config["Token:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(600),
                        signingCredentials: creds
                        );

                    return new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token)
                    };    
                }
            }catch(Exception ex)
            {
                _logger.LogError($"Exception thrown while creating JWT : {ex}");
                throw ex;
            }
            throw new ApplicationException("UNKNOWN_ERROR");
        }
    }
}
