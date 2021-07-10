using IdentityWithJwtDemo.Authentication;
using IdentityWithJwtDemo.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IdentityWithJwtDemo.Implementations
{
    public class PasswordRepository : IPasswordRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public PasswordRepository(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }
        public async Task<IdentityResult> ChangePassword(ApplicationUser userAlias, string orginalPassword, string newPassword)
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
