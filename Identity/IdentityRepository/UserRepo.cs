﻿using IdentityModels;
using IdentityModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdentityRepository
{
    public class UserRepo : BaseRepo<User>
    {
       
        private JwtRepo jwtRepo = new JwtRepo();
        private UserConvertRepo _userConvertRepo = new UserConvertRepo();
        public bool Add(User user)
        {
            user.Password = IdentityHelper.Encripty.EncryptString(user.Password);
            return mongoContext.Insert(user);

        }

        public object nativequery()
        {

            object rr = mongoContext.NativeQuery();
            return rr;
        }

        public UserView LoginByEmail(String Email, string Password)
        {
            UserView userView = null;
            Password = IdentityHelper.Encripty.EncryptString(Password);
            User user = mongoContext.SearchFor(p => p.Email == Email && p.Password == Password).FirstOrDefault();
            if (user != null)
            {
                userView = _userConvertRepo.UserToUserView(user);
                Result result  = jwtRepo.Add(user._id.ToString(), Guid.NewGuid().ToString(), DateTime.Now.AddDays(1));
                userView.Jwt = (Jwt)result.Data;
            }
            else
            {
                return userView;
            }

            return userView;
        }

        public bool Update(User user)
        {
            return mongoContext.Update(user);
        }

        public bool Delete(String Id)
        {
            User user = new User();
            user._id = Id;
            return mongoContext.Delete(user);
        }

        public User GetById(string ParentId, String Id)
        {
            User user = mongoContext.SearchFor(p => p.ParentId == ParentId && p._id == Id).FirstOrDefault();
            return user;
        }

        public User GetById(String Id)
        {
            User user = mongoContext.SearchFor(p => p._id == Id).FirstOrDefault();
            return user;
        }

        public List<User> GetByParentId(string ParentId)
        {
            List<User> userList = mongoContext.SearchFor(p => p.ParentId == ParentId).ToList();
            return userList;
        }

        public User GetByParentId(string ParentId, String Id)
        {
            User user = mongoContext.SearchFor(p => p.ParentId == ParentId && p._id == Id).First();
            return user;
        }

        public User GetByEmailAndParentId(string ParentId, String Email)
        {
            User user = mongoContext.SearchFor(p => p.Email == Email && p.ParentId == ParentId).FirstOrDefault();
            return user;
        }

        public User GetByEmail(String Email)
        {
            User user = mongoContext.SearchFor(p => p.Email == Email).FirstOrDefault();
            return user;
        }


        public User GetByEmail(string ParentId, string Email)
        {
            User user = mongoContext.SearchFor(p => p.ParentId == ParentId && p.Email == Email).FirstOrDefault();
            return user;
        }


        public bool AddUniqIndex()
        {
            List<string> list = new List<string>();
            list.Add("PatrentId");
            list.Add("Email");
            list.Add("ProjectCode");
            bool result = mongoContext.AddUniqIndex(list.ToArray());
            return result;
        }
    }
}
