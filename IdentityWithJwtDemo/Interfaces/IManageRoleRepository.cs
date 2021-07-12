using IdentityWithJwtDemo.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityWithJwtDemo.Interfaces
{
    public interface IManageRoleRepository //: IGenericRepository<ApplicationUser>
    {


        public Task<IEnumerable<ApplicationUser>> GetUsersByRole(IdentityRole identityRole);
        public Task<IdentityResult> GetUsersByRole(ApplicationUser applicationUser, IdentityRole identityRole);
        public Task<IdentityResult> RemoveUserFromRoleId(ApplicationUser applicationUser, string roleId);
        public Task<IEnumerable<string>> GetRoleNamesByUser(ApplicationUser applicationUser);


        



    }
}
