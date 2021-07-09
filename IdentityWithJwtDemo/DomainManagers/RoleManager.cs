using IdentityWithJwtDemo.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityWithJwtDemo.DomainManagers
{
    public class RoleManager
    {
        /*private readonly UserManager<ApplicationUser> _userManager;*/
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public RoleManager(/*UserManager<ApplicationUser> userManager,*/ RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            /*this._userManager = userManager;*/
            this._roleManager = roleManager;
            this._configuration = configuration;
        }
        public async Task<bool> RoleExistsAsync(String roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
        public async Task<IdentityResult> CreateRole(ApplicationUser user, IdentityRole identityRole)
        {
            return  await _roleManager.CreateAsync(identityRole);
        }
        public IEnumerable<IdentityRole> GetRoles()
        {
            return _roleManager.Roles;
        }
        public async Task<IdentityRole> GetRoleById(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }
        public async Task<IdentityResult> UpdateRole(IdentityRole identityRole)
        {
            return await _roleManager.UpdateAsync(identityRole);
        }
        public async Task<IdentityResult> DeleteRoleById(IdentityRole identityRole)
        {
            return await _roleManager.DeleteAsync(identityRole);
        }
        public async Task<IEnumerable<System.Security.Claims.Claim>> GetClaimsByRole(IdentityRole identityRole)
        {
            return await _roleManager.GetClaimsAsync(identityRole);
        }
        public async Task<IdentityResult> AddClaimToRole(System.Security.Claims.Claim claim, IdentityRole role)
        {
            return await _roleManager.AddClaimAsync(role, claim);
        }
        public async Task<IdentityResult> RemoveClaimFromRole(System.Security.Claims.Claim claim,IdentityRole identityRole)
        {
            return await _roleManager.RemoveClaimAsync(identityRole, claim);
        }

    }
}
