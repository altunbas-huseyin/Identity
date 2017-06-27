using IdentityModels;
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
        DapperManager dapperManager;

        public UserRepo(IConfiguration configuration) : base(configuration)
        {

            _configuration = configuration;
            jwtRepo = new JwtRepo(configuration);
            dapperManager = new DapperManager(_configuration);
        }

        public int Add(User user)
        {
            user.Password = IdentityHelper.Encripty.EncryptString(user.Password);
            //return mongoContext.Insert(user);
            int result = dapperManager.Insert<User>(connectionString, user);

            return result;
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
                Result result = jwtRepo.Add(user.Id, Guid.NewGuid().ToString(), DateTime.Now.AddDays(1));
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

        public bool Delete(long Id)
        {
            User user = new User();
            user.Id = Id;
            return mongoContext.Delete(user);
        }

        public User GetById(long ParentId, long Id)
        {
            User user = mongoContext.SearchFor(p => p.Parent_Id == ParentId && p.Id == Id).FirstOrDefault();
            return user;
        }

        public User GetById(long Id)
        {
            User user = mongoContext.SearchFor(p => p.Id == Id).FirstOrDefault();
            return user;
        }

        public List<User> GetByParentId(long ParentId)
        {
            List<User> userList = mongoContext.SearchFor(p => p.Parent_Id == ParentId).ToList();
            return userList;
        }

        public User GetByParentId(long ParentId, long Id)
        {
            User user = mongoContext.SearchFor(p => p.Parent_Id == ParentId && p.Id == Id).First();
            return user;
        }

        public User GetByEmailAndParentId(long ParentId, string Email)
        {
            User user = mongoContext.SearchFor(p => p.Email == Email && p.Parent_Id == ParentId).FirstOrDefault();
            return user;
        }

        public User GetByEmail(String Email)
        {
            User user = mongoContext.SearchFor(p => p.Email == Email).FirstOrDefault();
            return user;
        }


        public User GetByEmail(long ParentId, string Email)
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
