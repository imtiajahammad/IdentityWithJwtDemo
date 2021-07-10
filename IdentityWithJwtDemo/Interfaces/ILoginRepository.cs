using IdentityWithJwtDemo.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityWithJwtDemo.Interfaces
{
    public interface ILoginRepository//: IGenericRepository<ApplicationUser>
    {
        public async Task<JwtSecurityToken> Login(LoginViewModel model);
    }
}
