﻿using IdentityModels;
using IdentityModels.Permissions;
using IdentityModels.RolePermissions;
using IdentityModels.Roles;
using IdentityModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace IdentityRepository
{
    public class UserRepo : BaseRepo<User>
    {

        private JwtRepo jwtRepo;
        private UserConvertRepo _userConvertRepo = new UserConvertRepo();
        IConfiguration _configuration;
        public UserRepo(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
               jwtRepo = new JwtRepo(configuration);
        }

        public bool Add(User user)
        {

            string sss = DapperManager.getSqlParameterString<User>(user);
            user.Password = IdentityHelper.Encripty.EncryptString(user.Password);
            //return mongoContext.Insert(user);
            DapperManager dapperManager = new DapperManager(_configuration);
            object o = dapperManager.Insert<User>(connectionString, user);
            return true;
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
                Result result = jwtRepo.Add(user.Id.ToString(), Guid.NewGuid().ToString(), DateTime.Now.AddDays(1));
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
            user.Id = Id;
            return mongoContext.Delete(user);
        }

        public User GetById(string ParentId, String Id)
        {
            User user = mongoContext.SearchFor(p => p.Parent_Id == ParentId && p.Id == Id).FirstOrDefault();
            return user;
        }

        public User GetById(String Id)
        {
            User user = mongoContext.SearchFor(p => p.Id == Id).FirstOrDefault();
            return user;
        }

        public List<User> GetByParentId(string ParentId)
        {
            List<User> userList = mongoContext.SearchFor(p => p.Parent_Id == ParentId).ToList();
            return userList;
        }

        public User GetByParentId(string ParentId, String Id)
        {
            User user = mongoContext.SearchFor(p => p.Parent_Id == ParentId && p.Id == Id).First();
            return user;
        }

        public User GetByEmailAndParentId(string ParentId, String Email)
        {
            User user = mongoContext.SearchFor(p => p.Email == Email && p.Parent_Id == ParentId).FirstOrDefault();
            return user;
        }

        public User GetByEmail(String Email)
        {
            User user = mongoContext.SearchFor(p => p.Email == Email).FirstOrDefault();
            return user;
        }


        public User GetByEmail(string ParentId, string Email)
        {
            User user = mongoContext.SearchFor(p => p.Parent_Id == ParentId && p.Email == Email).FirstOrDefault();
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
