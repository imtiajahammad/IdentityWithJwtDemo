using IdentityWithJwtDemo.Authentication;
using IdentityWithJwtDemo.DomainRepositories;
using IdentityWithJwtDemo.Interfaces;
using IdentityWithJwtDemo.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityWithJwtDemo.Implementations
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : base(context)
        {
            this._userManager = userManager;
        }
        public async Task<StatusResult<string>> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new StatusResult<string> { Status = ResponseStatus.NotFound, Message = "user not found" };
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return new StatusResult<string> { Status = ResponseStatus.Success, Message = "User deleted" };
            }
            var errors = "";
            foreach (var a in result.Errors)
            {
                errors += "|" + a.Description.ToString();
            }
            return new StatusResult<string> { Status = ResponseStatus.Failed, Message = errors.ToString(), Result = String.Empty };
        }
        public async Task<StatusResult<ApplicationUser>> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new StatusResult<ApplicationUser> { Status = ResponseStatus.NotFound, Message = "user not found" };
            }
            return new StatusResult<ApplicationUser> { Status = ResponseStatus.Success, Message = "User found", Result = user };
        }
        public async Task<StatusResult<ApplicationUser>> UpdateUser(EditUserViewModel editUserViewModel)
        {
            var user = await _userManager.FindByIdAsync(editUserViewModel.id);
            if (user == null)
            {
                return new StatusResult<ApplicationUser> { Status = ResponseStatus.NotFound, Message = "user not found" };
            }
            user.UserName = editUserViewModel.name;
            user.Email = editUserViewModel.email;
            user.PhoneNumber = editUserViewModel.phoneNumber;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new StatusResult<ApplicationUser> { Status = ResponseStatus.Success, Message = "User updated" };
            }
            return new StatusResult<ApplicationUser> { Status = ResponseStatus.Failed, Message = "something went wrong" };
        }
        public async Task<ApplicationUser> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            return user;
        }
        public async Task<ApplicationUser> GetUserByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return null;
            }
            return user;
        }


        #region roles
        public async Task<IEnumerable<ApplicationUser>> GetUsersByRole(IdentityRole identityRole)
        {
            return await _userManager.GetUsersInRoleAsync(identityRole.Name);
        }
        public async Task<IdentityResult> GetUsersByRole(ApplicationUser applicationUser, IdentityRole identityRole)
        {
            return await _userManager.AddToRoleAsync(applicationUser, identityRole.Name);
        }
        public async Task<IdentityResult> RemoveUserFromRoleId(ApplicationUser applicationUser, string roleId)
        {
            return await _userManager.RemoveFromRoleAsync(applicationUser, roleId);
        }
        public async Task<IEnumerable<string>> GetRoleNamesByUser(ApplicationUser applicationUser)
        {
            return await _userManager.GetRolesAsync(applicationUser);
        }
        #endregion roles


        #region claims
        public async Task<IEnumerable<System.Security.Claims.Claim>> GetClaimsByUser(ApplicationUser applicationUser)
        {
            return await _userManager.GetClaimsAsync(applicationUser);
        }
        public async Task<IdentityResult> AddClaimToUser(ApplicationUser applicationUser, System.Security.Claims.Claim claim)
        {
            return await _userManager.AddClaimAsync(applicationUser, claim);
        }
        public async Task<IdentityResult> RemoveClaimFromUser(ApplicationUser applicationUser, System.Security.Claims.Claim claim)
        {
            return await _userManager.RemoveClaimAsync(applicationUser, claim);
        }

        #endregion claims

        public async Task<IdentityResult> ChangePassword(ApplicationUser applicationUser, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(applicationUser, oldPassword, newPassword);
        }
        public async Task<IdentityResult> ResetPasswordForUser(ApplicationUser user, string token, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return result;
        }
    }
}
