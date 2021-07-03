using IdentityWithJwtDemo.Authentication;
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

namespace IdentityWithJwtDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase/*ControllerBase*/
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthenticateController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._configuration = configuration;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    user=user.UserName,
                    claims=authClaims
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResult<string> { Status = ResponseStatus.Failed, Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResult<string> { Status = ResponseStatus.Failed, Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            return Ok(new StatusResult<string> { Status = ResponseStatus.Success, Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("register-admin")]
        [System.Web.Http.Authorize(Roles = "sadmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResult<string> { Status = ResponseStatus.Failed, Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResult<string> { Status = ResponseStatus.Failed, Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new StatusResult<string> { Status = ResponseStatus.Success, Message = "User created successfully!" });
        }


        [HttpDelete]
        [Route("deleteUser")]
        [System.Web.Http.Authorize(Roles = "hadmin")]
        public async Task<IActionResult> deleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "user not found" });
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(new StatusResult<string> { Status = ResponseStatus.Success, Message = "User deleted" });
            }
            var errors = "";
            foreach (var a in result.Errors)
            {
                errors += "|" + a.Description.ToString();
            }
            return Ok(new StatusResult<string> { Status = ResponseStatus.Failed, Message = errors.ToString(), Result = String.Empty });
        }


        [HttpGet]
        [Route("getUser")]
        public async Task<IActionResult> getUser(string id)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "user not found" });
                }
                return Ok(new StatusResult<ApplicationUser> { Status = ResponseStatus.Success, Message = "User found", Result = user });
            }
            return Ok(new StatusResult<ApplicationUser> { Status = ResponseStatus.Failed, Message = "something went wrong" });
        }
        [HttpPut]
        [Route("updateUser")]
        public async Task<IActionResult> updateUser(EditUserViewModel editUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(editUserViewModel.id);
                if (user == null)
                {
                    return NotFound(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "user not found" });
                }
                user.UserName = editUserViewModel.name;
                user.Email = editUserViewModel.email;
                user.PhoneNumber = editUserViewModel.phoneNumber;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Ok(new StatusResult<ApplicationUser> { Status = ResponseStatus.Success, Message = "User updated" });
                }
            }
            return Ok(new StatusResult<ApplicationUser> { Status = ResponseStatus.Failed, Message = "something went wrong" });
        }

        [HttpPost]
        [Route("changePassword")]
        public async Task<IActionResult> changePassword(string orginalPassword, string newPassword)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
                if (user == null)
                {
                    return NotFound(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "user not found" });
                }
                var result = await _userManager.ChangePasswordAsync(user, orginalPassword, newPassword);
                if (result.Succeeded)
                {
                    return Ok(new StatusResult<ApplicationUser> { Status = ResponseStatus.Success, Message = "password updated" });
                }
                var errors = "";
                foreach (var a in result.Errors)
                {
                    errors += "|" + a.Description.ToString();
                }
                return Ok(new StatusResult<string> { Status = ResponseStatus.Failed, Message = errors.ToString()});
            }
            return Ok(new StatusResult<ApplicationUser> { Status = ResponseStatus.Failed, Message = "something went wrong" });
        }

        
    }
    public class EditUserViewModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
    }
}
