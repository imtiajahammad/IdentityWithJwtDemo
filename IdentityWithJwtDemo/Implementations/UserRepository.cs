using IdentityWithJwtDemo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityWithJwtDemo.Implementations
{
    public class UserRepository : IUserRepository
    {
        public T Delete<T>(string id)
        {
            throw new NotImplementedException();
        }

        public T Delete<T>()
        {
            throw new NotImplementedException();
        }

        public T Get<T>()
        {
            throw new NotImplementedException();
        }

        public T Get<T>(T t)
        {
            throw new NotImplementedException();
        }

        public T Post<T>(T t)
        {
            throw new NotImplementedException();
        }

        public T Put<T>(T t)
        {
            throw new NotImplementedException();
        }
    }
}
