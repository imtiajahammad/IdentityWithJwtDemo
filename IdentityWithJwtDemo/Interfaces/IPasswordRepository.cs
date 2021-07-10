using IdentityWithJwtDemo.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityWithJwtDemo.Interfaces
{
    public interface IPasswordRepository //: IGenericRepository<ApplicationUser>
    {
        public Task<IdentityResult> ChangePassword(ApplicationUser userAlias, string orginalPassword, string newPassword);
        public string HashPassword(ApplicationUser userAlias, string passwordString);
    }
}
