using Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            UserType _userType = this.GetByName(Name);
            if (_userType == null)
            {
                UserType userType = new UserType();
                userType.Name = Name;
                return userTypeRepository.Insert(userType);
            }

            return true;
        }

        public UserType GetByName(String Name)
        {
            UserType userType = userTypeRepository.SearchFor(p => p.Name == Name).FirstOrDefault();

            return userType;
        }
    }
}
