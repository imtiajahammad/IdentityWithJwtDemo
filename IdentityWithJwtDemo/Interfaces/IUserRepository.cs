using IdentityWithJwtDemo.Authentication;
using IdentityWithJwtDemo.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityWithJwtDemo.Interfaces
{
    public interface IUserRepository:IGenericRepository<ApplicationUser>
    {
        public Task<StatusResult<string>> DeleteUser(string id);
        public Task<StatusResult<ApplicationUser>> GetUser(string id);
        public Task<StatusResult<ApplicationUser>> UpdateUser(EditUserViewModel editUserViewModel);
        public Task<ApplicationUser> GetUserById(string id);
        public Task<ApplicationUser> GetUserByUserName(string userName);


        #region roles
        public Task<IEnumerable<ApplicationUser>> GetUsersByRole(IdentityRole identityRole);
        public Task<IdentityResult> GetUsersByRole(ApplicationUser applicationUser, IdentityRole identityRole);
        public Task<IdentityResult> RemoveUserFromRoleId(ApplicationUser applicationUser, string roleId);
        public Task<IEnumerable<string>> GetRoleNamesByUser(ApplicationUser applicationUser);
        #endregion roles


        #region claims
        public Task<IEnumerable<System.Security.Claims.Claim>> GetClaimsByUser(ApplicationUser applicationUser);
        public Task<IdentityResult> AddClaimToUser(ApplicationUser applicationUser, System.Security.Claims.Claim claim);
        public Task<IdentityResult> RemoveClaimFromUser(ApplicationUser applicationUser, System.Security.Claims.Claim claim);

        #endregion claims

        public Task<IdentityResult> ChangePassword(ApplicationUser applicationUser, string oldPassword, string newPassword);
        public Task<IdentityResult> ResetPasswordForUser(ApplicationUser user, string token, string newPassword);
        
    }
}
