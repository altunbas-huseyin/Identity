
using IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdentityRepository
{
    public class UserTypeRepo
    {
        private MongoDbRepository<UserType> userTypeRepository = new MongoDbRepository<UserType>();
        public bool AddUniqIndex()
        {
            bool result = userTypeRepository.AddUniqIndex("Name");
            return result;
        }

        public string Add(string Name)
        {
            UserType userType = new UserType();
            userType.Name = Name;
            userTypeRepository.Insert(userType);
            return userType._id;
        }

        public UserType GetByName(String Name)
        {
            UserType userType = userTypeRepository.SearchFor(p => p.Name == Name).FirstOrDefault();

            return userType;
        }
    }
}
