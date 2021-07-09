using IdentityWithJwtDemo.Authentication;
using IdentityWithJwtDemo.Controllers;
using IdentityWithJwtDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityWithJwtDemo.Interfaces
{
    interface IRepository
    {
        /// <summary>
        /// to create/post a data
        /// </summary>
        /// <returns></returns>
        public T Post<T>(T t);

        /// <summary>
        /// to get list of data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>();

        /// <summary>
        /// get data by id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get<T>(T t);

        /// <summary>
        /// update data  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public T Put<T>(T t);

        /// <summary>
        /// delete data by id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Delete<T>(string id);


        /// <summary>
        /// delete list of data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Delete<T>();
    }
}
