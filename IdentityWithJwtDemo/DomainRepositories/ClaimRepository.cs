using IdentityWithJwtDemo.Authentication;
using IdentityWithJwtDemo.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityWithJwtDemo.DomainRepositories
{
    public class ClaimRepository: GenericRepository<Claim>, IClaimRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        #region _roleManager
        public ClaimRepository(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager) : base(context)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
        }
        public async Task<IEnumerable<Claim>> GetClaimsByRole(IdentityRole identityRole)
        {
            return await _roleManager.GetClaimsAsync(identityRole);
        }
        public async Task<IdentityResult> AddClaimToRole(Claim claim, IdentityRole role)
        {
            return await _roleManager.AddClaimAsync(role, claim);
        }
        public async Task<IdentityResult> RemoveClaimFromRole(Claim claim, IdentityRole identityRole)
        {
            return await _roleManager.RemoveClaimAsync(identityRole, claim);
        }
        #endregion _roleManager


        #region _userManager
        public async Task<IEnumerable<Claim>> GetClaimsByUser(ApplicationUser applicationUser)
        {
            return await _userManager.GetClaimsAsync(applicationUser);
        }
        public async Task<IdentityResult> AddClaimToUser(ApplicationUser applicationUser, Claim claim)
        {
            return await _userManager.AddClaimAsync(applicationUser, claim);
        }
        public async Task<IdentityResult> RemoveClaimFromUser(ApplicationUser applicationUser, Claim claim)
        {
            return await _userManager.RemoveClaimAsync(applicationUser, claim);
        }
        public async Task<IdentityResult> RemoveUserFromRoleId(ApplicationUser applicationUser, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(applicationUser, oldPassword, newPassword);
        }
        #endregion _userManager
    }
}
