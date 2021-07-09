using IdentityWithJwtDemo.Authentication;
using IdentityWithJwtDemo.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityWithJwtDemo.Interfaces
{
    interface IAdminRepository:IRepository
    {
        public T PostItHere<T>(T t);
    }
    interface IUserRepository : IRepository
    {

    }
    interface IRolesRepository : IRepository
    {

    }
    interface IManageRoleRepository : IRepository
    {

    }
    interface IClaimRepository : IRepository
    {

    }
    interface IAuthenticationRepository : IRepository
    {

    }
    interface IPasswordRepository : IRepository
    {

    }
}
