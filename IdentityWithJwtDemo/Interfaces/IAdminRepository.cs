using IdentityWithJwtDemo.Authentication;
using IdentityWithJwtDemo.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityWithJwtDemo.Interfaces
{
    public interface IAdminRepository //: IGenericRepository<ApplicationUser>
    {
        public Task<StatusResult<string>> RegisterAdmin(RegisterViewModel model);
    }
}
