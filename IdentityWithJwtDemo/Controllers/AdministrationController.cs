using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityWithJwtDemo.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityWithJwtDemo.Controllers
{
    public class AdministrationController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }


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
                    return Ok(new Response { Status = "Success", Message = "Role created successfully!" });
                }
                var errors = "";
                foreach(var a in result.Errors)
                {
                    errors += "|" + a.Description.ToString();
                }
                return Ok(new Response { Status = "Error", Message = errors.ToString() });
            }
            return Ok(new Response { Status = "Failed", Message = "Something went wrong" });
        }
    }




    public class CreateRoleViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string RoleName { get; set; }
    }
}
