using IdentityWithJwtDemo.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace IdentityWithJwtDemo.Interfaces
{
    public interface IClaimRepository : IGenericRepository<Claim>
    {
        public Task<IEnumerable<Claim>> GetClaimsByRole(IdentityRole identityRole);

        public Task<IdentityResult> AddClaimToRole(System.Security.Claims.Claim claim, IdentityRole role);

        public Task<IdentityResult> RemoveClaimFromRole(System.Security.Claims.Claim claim, IdentityRole identityRole);

        public Task<IEnumerable<Claim>> GetClaimsByUser(ApplicationUser applicationUser);

        public Task<IdentityResult> AddClaimToUser(ApplicationUser applicationUser, System.Security.Claims.Claim claim);

        public Task<IdentityResult> RemoveClaimFromUser(ApplicationUser applicationUser, System.Security.Claims.Claim claim);

        public Task<IdentityResult> RemoveUserFromRoleId(ApplicationUser applicationUser, string oldPassword, string newPassword);

    }
}
