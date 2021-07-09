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


        public async Task<IdentityResult> changePassword(ApplicationUser userAlias,string orginalPassword, string newPassword)
        {
                var result = await _userManager.ChangePasswordAsync(userAlias, orginalPassword, newPassword);
                return result;
        }
    }
}
