using GyereHazaApi4.Authentication;
using GyereHazaApi4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GyereHazaApi4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> usermanager;
        private readonly RoleManager<IdentityRole> rolemanager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<ApplicationUser> usermanager, RoleManager<IdentityRole> rolemanager, IConfiguration configuration)
        {
            this.usermanager = usermanager;
            this.rolemanager= rolemanager;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("Register")]
        public async Task <IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExist = await usermanager.FindByNameAsync(model.UserName);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { status = "Error", Message = "User already exist" });
            }
            else
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.UserName
                };
                var result = await usermanager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { status = "Error", Message = "User creation failed" });
                }
                return Ok(new Response { status = "Success", Message = "User created successfully" });
            }
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await usermanager.FindByNameAsync(model.Username);
            if(user!=null&&await usermanager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await usermanager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                };
                foreach (var userrole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userrole));
                }
                var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims : authClaims,
                    signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256));
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            return Unauthorized();
        }

    }
}
