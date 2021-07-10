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
        /*public  T RoleExistsAsync(T roleName);

        public  T CreateRole(T user, T identityRole);

        public T GetRoles();

        public  T GetRoleById(T id);

        public  T UpdateRole(T identityRole);

        public  T DeleteRoleById(T identityRole);

        public  T GetClaimsByRole(T identityRole);

        public  T AddClaimToRole(T claim, T role);

        public  T RemoveClaimFromRole(T claim, T identityRole);*/
        public Task<bool> RoleExistsAsync(string roleName);

        public Task<IdentityResult> CreateRole(ApplicationUser user, IdentityRole identityRole);

        public IEnumerable<IdentityRole> GetRoles();

        public Task<IdentityRole> GetRoleById(string id);

        public Task<IdentityResult> UpdateRole(IdentityRole identityRole);

        public Task<IdentityResult> DeleteRoleById(IdentityRole identityRole);

        public Task<IEnumerable<Claim>> GetClaimsByRole(IdentityRole identityRole);

        public Task<IdentityResult> AddClaimToRole(System.Security.Claims.Claim claim, IdentityRole role);

        public Task<IdentityResult> RemoveClaimFromRole(System.Security.Claims.Claim claim, IdentityRole identityRole);
    }
}