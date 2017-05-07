using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class UserRepo
    {
        private MongoDbRepository<User> userRepository = new MongoDbRepository<User>();
        private string key = "E546C8DF278CD5931069B522E695D4F2";
        public bool Add(string Email, string Password, string ProjectName, string Name, string SurName, User.UserType Type)
        {
            User user = new User();
            user.ProjectName = ProjectName;
            user.Email = Email;
            user.Password = IdentityHelper.Encripty.EncryptString(Password);
            user.Name = Name;
            user.SurName = SurName;
            user.Type = Type;
            user.CreateDate = DateTime.Now;

            return userRepository.Insert(user);
        }

        public User LoginByEmail(string ProjectName, String Email, string Password)
        {
            Password = IdentityHelper.Encripty.EncryptString(Password);
            User user = userRepository.SearchFor(p => p.ProjectName == ProjectName && p.Email == Email && p.Password == Password).FirstOrDefault();
            user.Password = "";
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
            User user = userRepository.SearchFor(p => p.Id == new Guid(Id)).First();

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
