using IdentityWithJwtDemo.Authentication;
using IdentityWithJwtDemo.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace IdentityWithJwtDemo.DomainManagers
{
    public class UserManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        /*private readonly RoleManager<IdentityRole> _roleManager;*/
        private readonly IConfiguration _configuration;
        public UserManager(UserManager<ApplicationUser> userManager/*, RoleManager<IdentityRole> roleManager*/, IConfiguration configuration)
        {
            this._userManager = userManager;
            /*this._roleManager = roleManager;*/
            this._configuration = configuration;
        }
        /*public async Task<StatusResult<string>> Register([System.Web.Http.FromBody] RegisterViewModel model)
        {
            StatusResult<string> status = new StatusResult<string>();
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
            {
                return new StatusResult<string> { Status = ResponseStatus.Failed, Message = "User already exists!" };
            }
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return new StatusResult<string> { Status = ResponseStatus.Failed, Message = "User creation failed! Please check user details and try again." };
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            return new StatusResult<string> { Status = ResponseStatus.Success, Message = "User created successfully!" };
        }*/
        public async Task<StatusResult<string>> deleteUser(string id)
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
        public async Task<StatusResult<ApplicationUser>> getUser(string id)
        {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return new StatusResult<ApplicationUser> { Status = ResponseStatus.NotFound, Message = "user not found" };
                }
                return new StatusResult<ApplicationUser> { Status = ResponseStatus.Success, Message = "User found", Result = user };
        }
        public async Task<StatusResult<ApplicationUser>> updateUser(EditUserViewModel editUserViewModel)
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
        public async Task<ApplicationUser> getUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            return user;
        }
        public async Task<ApplicationUser> getUserByUserName(string userName)
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
        public async Task<IdentityResult> AddClaimToUser(ApplicationUser applicationUser, System.Security.Claims.Claim  claim)
        {
            return await _userManager.AddClaimAsync(applicationUser, claim);
        }
        public async Task<IdentityResult> RemoveClaimFromUser(ApplicationUser applicationUser, System.Security.Claims.Claim claim)
        {
            return await _userManager.RemoveClaimAsync(applicationUser, claim);
        }
        public async Task<IdentityResult> RemoveUserFromRoleId(ApplicationUser applicationUser, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(applicationUser, oldPassword, newPassword);
        }
        #endregion claims

    }
}
