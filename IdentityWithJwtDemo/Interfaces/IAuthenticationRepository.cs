using IdentityWithJwtDemo.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityWithJwtDemo.Interfaces
{
    interface IAuthenticationRepository : IGenericRepository<ApplicationUser>
    {
    }
}
