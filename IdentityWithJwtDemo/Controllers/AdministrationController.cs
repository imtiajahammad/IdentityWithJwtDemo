using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityWithJwtDemo.Authentication;
using IdentityWithJwtDemo.Implementations;
using IdentityWithJwtDemo.Interfaces;
using IdentityWithJwtDemo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityWithJwtDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles ="sadmin,hadmin")]
    public class AdministrationController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRoleRepository _roleRepository;
        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IRoleRepository roleRepository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _roleRepository = roleRepository;
        }
        [Route("CreateRole")]
        [HttpPost]
        public async Task<IActionResult> createRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return Ok(new StatusResult<string> { Status = ResponseStatus.Success, Message = "Role created successfully!",Result=String.Empty});
                }
                var errors = "";
                foreach(var a in result.Errors)
                {
                    errors += "|" + a.Description.ToString();
                }
                return Ok(new StatusResult<string> { Status = ResponseStatus.Failed, Message = errors.ToString(),Result=String.Empty });
            }
            return BadRequest(new StatusResult<string> { Status = ResponseStatus.Failed, Message = "Something went wrong" });
        }
        [Route("GetRoles")]
        [HttpGet]
        public IActionResult getRoleList()
        {
            StatusResult<List<CreateRoleViewModel>> st= new StatusResult<List<CreateRoleViewModel>>();
            List<CreateRoleViewModel> list = new List<CreateRoleViewModel>();
            
            var roles =  _roleManager.Roles;
            foreach(var a in roles)
            {
                CreateRoleViewModel crvm = new CreateRoleViewModel();
                crvm.RoleId = a.Id.ToString();
                crvm.RoleName = a.Name.ToString();
                list.Add(crvm);
            }
            if (roles != null)
            {
                return Ok(new StatusResult<List<CreateRoleViewModel>> { Status=ResponseStatus.Success,Message="success",Result=list });
            }
            return NotFound(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "Something went wrong" });
        }
        [HttpGet]
        [Route("GetRole")]
        public async Task<IActionResult> getRole(string id)
        {
            StatusResult<CreateRoleViewModel> status = new StatusResult<CreateRoleViewModel>();
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "Data not found" });
            }
            var model = new CreateRoleViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
            return Ok(new StatusResult<CreateRoleViewModel> { Status = ResponseStatus.Success,Message = "Data found",Result = model });
        }
        [HttpPut]
        [Route("UpdateRole")]
        public  async Task<IActionResult> updateRole(CreateRoleViewModel createRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(createRoleViewModel.RoleId);
                if (role == null)
                {
                    return NotFound(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "Data not found" });
                }
                role.Name = createRoleViewModel.RoleName;
                IdentityResult result=await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return BadRequest(new StatusResult<string>() { Status = ResponseStatus.Failed, Message = "Role update failed" });
                }
                var errors = "";
                foreach (var a in result.Errors)
                {
                    errors += "|" + a.Description.ToString();
                }
                return Ok(new StatusResult<string> { Status = ResponseStatus.Failed, Message = errors.ToString(), Result = String.Empty });
            }
            return BadRequest(new StatusResult<string>() { Status = ResponseStatus.Failed, Message = "Something went wrong" });
        }
        [HttpDelete]
        [Route("DeleteRole")]
        [Authorize(Policy = "DeleteRoleClaim")]
        public async Task<IActionResult> deleteRole(CreateRoleViewModel createRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(createRoleViewModel.RoleId);
                if (role == null)
                {
                    return NotFound(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "Data not found" });
                }
                var users=await _userManager.GetUsersInRoleAsync(createRoleViewModel.RoleName);
                if (users != null)
                {
                    return BadRequest(new StatusResult<string>() { Status = ResponseStatus.Failed, Message = "Role delete failed" });
                }
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return Ok(new StatusResult<string> { Status = ResponseStatus.Success, Message = "Role deleted", Result = String.Empty });
                }
                var errors = "";
                foreach (var a in result.Errors)
                {
                    errors += "|" + a.Description.ToString();
                }
                return Ok(new StatusResult<string> { Status = ResponseStatus.Failed, Message = errors.ToString(), Result = String.Empty });
            }
            return BadRequest(new StatusResult<string>() { Status = ResponseStatus.Failed, Message = "Something went wrong" });
        }
        [Route("AddUserToRole")]
        [HttpPost]
        public async Task<IActionResult> addUserToRole(string userId,string roleId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return NotFound(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "User not found" });
                }
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role == null)
                {
                    return NotFound(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "Role not found" });
                }
                var result = await _userManager.AddToRoleAsync(user, role.Name);
                if (result.Succeeded)
                {
                    return Ok(new StatusResult<string> { Status = ResponseStatus.Success, Message = "Role added to user", Result = String.Empty });
                }
                var errors = "";
                foreach (var a in result.Errors)
                {
                    errors += "|" + a.Description.ToString();
                }
                return Ok(new StatusResult<string> { Status = ResponseStatus.Failed, Message = errors.ToString(), Result = String.Empty });
            }
            return BadRequest(new StatusResult<string>() { Status = ResponseStatus.Failed, Message = "Something went wrong" });
        }
        [HttpPost]
        [Route("RemoveRoleFromUser")]
        public async Task<IActionResult> RemoveRoleFromUser(string roleId,string userId)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                ApplicationUser user;
                if (role != null)
                {
                    user = await _userManager.FindByIdAsync(userId);
                    if (user == null)
                    {
                        return NotFound(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "user not found" });
                    }
                }
                else
                {
                    return NotFound(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "role not found" });
                }
                var result =await  _userManager.RemoveFromRoleAsync(user, roleId);
                if (result.Succeeded)
                {
                    return NotFound(new StatusResult<string> { Status = ResponseStatus.Success, Message = "Role removed from user" });
                }
                var errors = "";
                foreach (var a in result.Errors)
                {
                    errors += "|" + a.Description.ToString();
                }
                return Ok(new StatusResult<string> { Status = ResponseStatus.Failed, Message = errors.ToString(), Result = String.Empty });
            }
            return BadRequest(new StatusResult<string>() { Status = ResponseStatus.Failed, Message = "Something went wrong" });
        }
        [HttpGet]
        [Route("GetUsersByRole")]
        public async Task<IActionResult> GetUsersByRole(string roleId)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role == null)
                {
                    return NotFound(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "role not found" });
                }
                var users = await _userManager.GetUsersInRoleAsync(roleId);
                if (users == null)
                {
                    return NotFound(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "user not found" });
                }
                return Ok(new StatusResult<List<ApplicationUser>>() { Status = ResponseStatus.Failed, Message = "users fetched", Result =(List<ApplicationUser>)users });

                
            }
            return BadRequest(new StatusResult<string>() { Status = ResponseStatus.Failed, Message = "Something went wrong" });
        }
        [HttpGet]
        [Route("GetRolesByUser")]
        public async Task<IActionResult> GetRolesByUser(string userId)
        {
            if (ModelState.IsValid)
            {
                var user= await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return NotFound(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "user not found" });
                }
                var roles =await _userManager.GetRolesAsync(user);
                if(roles == null)
                {
                    return NotFound(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "roles not found" });
                }
            }
            return BadRequest(new StatusResult<string>() { Status = ResponseStatus.Failed, Message = "Something went wrong" });
        }
        [HttpPost]
        [Route("addClaimToRole")]
        public async Task<IActionResult> addClaimToRole(string claimName,string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "role not found" });
            }
            var claims= _roleManager.GetClaimsAsync(role);
            if(claims.Result.Contains(new System.Security.Claims.Claim(claimName, claimName)))
            {
                return Ok(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "claim already exists" });
            }
            var result = await _roleManager.AddClaimAsync(role , new System.Security.Claims.Claim(claimName, claimName));
            if (result.Succeeded)
            {
                return Ok(new StatusResult<string> { Status = ResponseStatus.Success, Message = "claim added to role" });
            }
            return BadRequest(new StatusResult<string>() { Status = ResponseStatus.Failed, Message = "Something went wrong" });
        }
        [HttpPost]
        [Route("removeClaimToRole")]
        public async Task<IActionResult> removeClaimToRole(string claimName, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "role not found" });
            }
            var claims = _roleManager.GetClaimsAsync(role);
            if (!claims.Result.Contains(new System.Security.Claims.Claim(claimName, claimName)))
            {
                return Ok(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "claim does not exist on role" });
            }
            var result = await _roleManager.RemoveClaimAsync(role, new System.Security.Claims.Claim(claimName, claimName));
            if (result.Succeeded)
            {
                return Ok(new StatusResult<string> { Status = ResponseStatus.Success, Message = "claim removed from role" });
            }
            return BadRequest(new StatusResult<string>() { Status = ResponseStatus.Failed, Message = "Something went wrong" });
        }
        [HttpPost]
        [Route("addClaimToRole")]
        public async Task<IActionResult> addClaimToUser(string claimName, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "user not found" });
            }
            var claims = _userManager.GetClaimsAsync(user);
            if (claims.Result.Contains(new System.Security.Claims.Claim(claimName, claimName)))
            {
                return Ok(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "claim already exists" });
            }
            var result = await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim(claimName, claimName));
            if (result.Succeeded)
            {
                return Ok(new StatusResult<string> { Status = ResponseStatus.Success, Message = "claim added to user" });
            }
            return BadRequest(new StatusResult<string>() { Status = ResponseStatus.Failed, Message = "Something went wrong" });
        }
        [HttpPost]
        [Route("removeClaimFromUser")]
        public async Task<IActionResult> removeClaimFromUser(string claimName, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "user not found" });
            }
            var claims = _userManager.GetClaimsAsync(user);
            if (!claims.Result.Contains(new System.Security.Claims.Claim(claimName, claimName)))
            {
                return Ok(new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "claim does not exist on user" });
            }
            var result = await _userManager.RemoveClaimAsync(user, new System.Security.Claims.Claim(claimName, claimName));
            if (result.Succeeded)
            {
                return Ok(new StatusResult<string> { Status = ResponseStatus.Success, Message = "claim removed to user" });
            }
            return BadRequest(new StatusResult<string>() { Status = ResponseStatus.Failed, Message = "Something went wrong" });
        }
    }
    
}
