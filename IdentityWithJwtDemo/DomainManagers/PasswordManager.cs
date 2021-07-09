using IdentityWithJwtDemo.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityWithJwtDemo.DomainManagers
{
    public class PasswordManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public PasswordManager(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._configuration = configuration;
        }


        public async Task<StatusResult<ApplicationUser>> changePassword(ApplicationUser userAlias,string orginalPassword, string newPassword)
        {
                var user = await _userManager.FindByNameAsync(userAlias.UserName);
                if (user == null)
                {
                    return new StatusResult<ApplicationUser> { Status = ResponseStatus.NotFound, Message = "user not found" };
                }
                var result = await _userManager.ChangePasswordAsync(user, orginalPassword, newPassword);
                if (result.Succeeded)
                {
                    return new StatusResult<ApplicationUser> { Status = ResponseStatus.Success, Message = "password updated" };
                }
                var errors = "";
                foreach (var a in result.Errors)
                {
                    errors += "|" + a.Description.ToString();
                }
                return new StatusResult<ApplicationUser> { Status = ResponseStatus.Failed, Message = errors.ToString() };
        }
    }
}
