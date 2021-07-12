using IdentityWithJwtDemo.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityWithJwtDemo.Interfaces
{
    public interface IRoleRepository : IGenericRepository<IdentityRole>
    {
        public Task<bool> RoleExistsAsync(string roleName);
        public Task<IdentityRole> GetRoleById(string id);
        public Task<IdentityResult> AddClaimToRole(System.Security.Claims.Claim claim, IdentityRole role);
        public Task<IdentityResult> RemoveClaimFromRole(System.Security.Claims.Claim claim, IdentityRole identityRole);
        public Task<IEnumerable<Claim>> GetClaimsByRole(IdentityRole identityRole);
        public Task<IdentityResult> CreateRole(IdentityRole identityRole);
        public Task<IdentityResult> DeleteRoleById(IdentityRole identityRole);
        public Task<IdentityResult> UpdateRole(IdentityRole identityRole);
        public IEnumerable<IdentityRole> GetRoles();
    }
}