using IdentityModels.Users;
using System;
using System.Collections.Generic;
using System.Text;
using IdentityModels;

namespace IdentityRepository
{
    public class TestRepo : IBaseRepo<User>
    {
        public Result Add(User entity)
        {
            throw new NotImplementedException();
        }

        public Result Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public User FindById(string Id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll(string UserId)
        {
            throw new NotImplementedException();
        }

        public User GetByUserId(string UserId)
        {
            throw new NotImplementedException();
        }

        public Result Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
