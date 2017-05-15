using IdentityModels;
using IdentityModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdentityRepository
{
    public class UserRepo
    {
        private MongoDbRepository<User> userRepository = new MongoDbRepository<User>();

        public Guid Add(User user)
        {
            userRepository.Insert(user);
            return user.Id;
        }

        public User LoginByEmail(String Email, string Password)
        {
            Password = IdentityHelper.Encripty.EncryptString(Password);
            User user = userRepository.SearchFor(p => p.Email == Email && p.Password == Password).FirstOrDefault();
            if (user != null)
            { user.Password = ""; }

            return user;
        }

        public bool Update(User user)
        {
            return userRepository.Update(user);
        }

        public bool Delete(String Id)
        {
            User user = new User();
            user.Id = Guid.Parse(Id);
            return userRepository.Delete(user);
        }

        public User GetById(String Id)
        {
            User user = userRepository.SearchFor(p => p.Id == new Guid(Id)).FirstOrDefault();

            return user;
        }

        public User GetByEmailAndParentId(String Email, string ParentId)
        {
            User user = userRepository.SearchFor(p => p.Email == Email && p.ParentId == ParentId).FirstOrDefault();
            return user;
        }

        public User GetByEmail(String Email)
        {
            User user = userRepository.SearchFor(p => p.Email == Email).FirstOrDefault();
            return user;
        }

        public bool AddUniqIndex()
        {
            List<string> list = new List<string>();
            list.Add("PatrentId");
            list.Add("Email");
            bool result = userRepository.AddUniqIndex(list.ToArray());
            return result;
        }
    }
}
