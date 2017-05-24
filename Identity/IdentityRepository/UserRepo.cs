﻿using IdentityModels;
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
        private JwtRepo jwtRepo = new JwtRepo();
        public string Add(User user)
        {
            user.Password = IdentityHelper.Encripty.EncryptString(user.Password);
            userRepository.Insert(user);
            return user._id;
        }

        public object nativequery()
        {

           object rr = userRepository.NativeQuery();
            return rr;
        }

        public UserView LoginByEmail(String Email, string Password)
        {
            UserView userView = null;
            Password = IdentityHelper.Encripty.EncryptString(Password);
            User user = userRepository.SearchFor(p => p.Email == Email && p.Password == Password).FirstOrDefault();
            if (user != null)
            {
                userView = this.UserToUserView(user);
                userView.Jwt = jwtRepo.Add(user._id.ToString(), Guid.NewGuid().ToString(), DateTime.Now.AddDays(1));
            }
            else
            {
                return userView;
            }

            return userView;
        }

        public UserView UserToUserView(User user)
        {
            UserView userView = new UserView();
            userView.Email = user.Email;
            userView.Extra1 = user.Extra1;
            userView.Extra2 = user.Extra2;
            userView.ProjectName = user.ProjectName;
            userView.ProjectCode = user.ProjectCode;
            userView._id = user._id;
            userView.ProjectName = user.ProjectName;
            userView.Role = user.Role;
            userView.Name = user.Name;
            userView.SurName = user.SurName;
            userView.UpdateDate = user.UpdateDate;
            userView.ParentId = user.ParentId;
            userView.Status = user.Status;

            return userView;
        }

        public bool Update(User user)
        {
            return userRepository.Update(user);
        }

        public bool Delete(String Id)
        {
            User user = new User();
            user._id = Id;
            return userRepository.Delete(user);
        }

        public List<User> Get(string ParentId)
        {
            List<User> userList = userRepository.SearchFor(p => p.ParentId == ParentId).ToList();
            return userList;
        }

        public User GetById(string ParentId, String Id)
        {
            User user = userRepository.SearchFor(p => p.ParentId == ParentId && p._id == Id).FirstOrDefault();
            return user;
        }

        public User GetById(String Id)
        {
            User user = userRepository.SearchFor(p => p._id == Id).FirstOrDefault();
            return user;
        }

        public User GetByParentId(string ParentId)
        {
            User user = userRepository.SearchFor(p => p.ParentId == ParentId).FirstOrDefault();
            return user;
        }

        public User GetByEmailAndParentId(string ParentId, String Email)
        {
            User user = userRepository.SearchFor(p => p.Email == Email && p.ParentId == ParentId).FirstOrDefault();
            return user;
        }

        public User GetByEmail(String Email)
        {
            User user = userRepository.SearchFor(p => p.Email == Email).FirstOrDefault();
            return user;
        }


        public User GetByEmail(string ParentId, string Email)
        {
            User user = userRepository.SearchFor(p => p.ParentId == ParentId && p.Email == Email).FirstOrDefault();
            return user;
        }


        public bool AddUniqIndex()
        {
            List<string> list = new List<string>();
            list.Add("PatrentId");
            list.Add("Email");
            list.Add("ProjectCode");
            bool result = userRepository.AddUniqIndex(list.ToArray());
            return result;
        }
    }
}
