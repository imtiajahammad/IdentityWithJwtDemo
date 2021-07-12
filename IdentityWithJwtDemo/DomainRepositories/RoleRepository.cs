using IdentityWithJwtDemo.Authentication;
using IdentityWithJwtDemo.DomainRepositories;
using IdentityWithJwtDemo.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityWithJwtDemo.Implementations
{
    public class RoleRepository : GenericRepository<IdentityRole>, IRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleRepository(ApplicationDbContext context, RoleManager<IdentityRole> roleManager) : base(context)
        {
            this._roleManager = roleManager;
        }
        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
        public async Task<IdentityResult> CreateRole(IdentityRole identityRole)
        {
            return await _roleManager.CreateAsync(identityRole);
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
        public async Task<IEnumerable<Claim>> GetClaimsByRole(IdentityRole identityRole)
        {
            return await _roleManager.GetClaimsAsync(identityRole);
        }
        public async Task<IdentityResult> AddClaimToRole(System.Security.Claims.Claim claim, IdentityRole role)
        {
            return await _roleManager.AddClaimAsync(role, claim);
        }
        public async Task<IdentityResult> RemoveClaimFromRole(System.Security.Claims.Claim claim, IdentityRole identityRole)
        {
            return await _roleManager.RemoveClaimAsync(identityRole, claim);
        }
        public async Task<IdentityResult> Post(IdentityRole t)
        {
            return await _roleManager.CreateAsync(t);
        }
        public IEnumerable<IdentityRole> Get()
        {
            return _roleManager.Roles;
        }
        public async Task<IdentityRole> Get(string t)
        {
            return await _roleManager.FindByIdAsync(t);
        }
        public async Task<IdentityResult> Put(IdentityRole t)
        {
            return await _roleManager.UpdateAsync(t);
        }
        public async Task<IdentityResult> Delete(IdentityRole t)
        {
            return await _roleManager.DeleteAsync(t);
        }
    }
}
