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
        private readonly IConfiguration _configuration;
        public PasswordManager(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._configuration = configuration;
        }
        public async Task<IdentityResult> ChangePassword(ApplicationUser userAlias,string orginalPassword, string newPassword)
        {
                var result = await _userManager.ChangePasswordAsync(userAlias, orginalPassword, newPassword);
                return result;
        }
        public string HashPassword(ApplicationUser userAlias, string passwordString)
        {
            var result = _userManager.PasswordHasher.HashPassword(userAlias, passwordString);
            return result;
        }
    }
}
