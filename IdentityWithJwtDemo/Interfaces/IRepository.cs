using IdentityWithJwtDemo.Authentication;
using IdentityWithJwtDemo.Controllers;
using IdentityWithJwtDemo.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityWithJwtDemo.Interfaces
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// to create/post a data
        /// </summary>
        /// <returns></returns>
        public T Post(T t);

        /// <summary>
        /// to get list of data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get();

        /// <summary>
        /// get data by id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(T t);

        /// <summary>
        /// update data  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public T Put(T t);

        /// <summary>
        /// delete data by id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Delete(string id);


        /// <summary>
        /// delete list of data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Delete();
    }

    public interface IRoleRepository0<TResult, TUser, TRole,TClaim,Tid> : IRepository<TResult>
    where TResult : class where TUser : class where TRole : class where TClaim : class where Tid : class
    {
        /*public Task<bool> RoleExistsAsync(string roleName);

        public Task<TResult> CreateRole(TUser user, TRole identityRole);

        public IEnumerable<TRole> GetRoles();

        public Task<TRole> GetRoleById(string id);*/
        //--------------------------------------------------------------

        public Task<bool> RoleExistsAsync(Tid roleName);

        public Task<TResult> CreateRole(TUser user, TRole identityRole);

        public IEnumerable<TRole> GetRoles();

        public Task<TRole> GetRoleById(Tid id);

        /*public Task<TResult> UpdateRole(TRole identityRole);

        public Task<TResult> DeleteRoleById(TRole identityRole);

        public Task<IEnumerable<TClaim>> GetClaimsByRole(TRole identityRole);

        public Task<TResult> AddClaimToRole(TClaim claim, TRole role);

        public Task<TResult> RemoveClaimFromRole(TClaim claim, TRole identityRole);*/
    }



    public class RoleManager0 : IRoleRepository0<IdentityResult, ApplicationUser, IdentityRole,Claim,String>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleManager0(RoleManager<IdentityRole> roleManager)
        {
            this._roleManager = roleManager;
        }
        public IdentityResult Post(IdentityResult t)
        {
            throw new NotImplementedException();
        }

        public IdentityResult Get()
        {
            throw new NotImplementedException();
        }

        public IdentityResult Get(IdentityResult t)
        {
            throw new NotImplementedException();
        }

        public IdentityResult Put(IdentityResult t)
        {
            throw new NotImplementedException();
        }

        public IdentityResult Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IdentityResult Delete()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
        public async Task<IdentityResult> CreateRole(ApplicationUser user, IdentityRole identityRole)
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
    }

}
