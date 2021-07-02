using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityWithJwtDemo.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityWithJwtDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministrationController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [Route("CreateRole")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
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
    }



    




    public class CreateRoleViewModel
    {
        public string RoleId { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string RoleName { get; set; }
    }
}
