using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class UserTypeRepo
    {
        private MongoDbRepository<UserType> userTypeRepository = new MongoDbRepository<UserType>();
        public bool AddUniqIndex()
        {
            bool result = userTypeRepository.AddUniqIndex("Name");
            return result;
        }

        public bool Add(string Name)
        {
            UserType userType = new UserType();
            userType.Name = Name;
            return userTypeRepository.Insert(userType);
        }
    }
}
