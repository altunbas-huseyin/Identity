using IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdentityRepository
{
    public class UserRepo
    {
        private MongoDbRepository<User> userRepository = new MongoDbRepository<User>();

        public bool Add(User user)
        {
            User _user = this.GetByEmailAndParentId(user.Email, user.ParentId);
            if (_user == null)
            {
                user.Password = IdentityHelper.Encripty.EncryptString(user.Password);
                user.CreateDate = DateTime.Now;
                return userRepository.Insert(user);
            }
            return true;
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
            list.Add("ProjectName");
            list.Add("Email");
            bool result = userRepository.AddUniqIndex(list.ToArray());
            return result;
        }
    }
}
