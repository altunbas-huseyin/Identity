using IdentityModels.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IdentityModels.Roles;

namespace IdentityRepository
{
    public class UserRoleRepo
    {
        private MongoDbRepository<User> userRepository = new MongoDbRepository<User>();
        private RoleRepo roleRepo = new RoleRepo();
        private UserRepo userRepo = new UserRepo();

        public bool UserAddRole(string ParentId, string UserId, string RoleId)
        {
            User user = userRepo.GetById(ParentId, UserId);
            if (user == null)
            { return false; }

            //Rol kullanıcıya daha önce atanmış ise bir işlem yapılmıyor
            bool result = IsAddedRole(user, RoleId);
            if (result)
            { return true; }

            Role role = roleRepo.GetById(ParentId, RoleId);
            if (user.Role.Count < 1)
            { user.Role = new List<Role>(); }
            user.Role.Add(role);

            bool userUpdateResult = userRepo.Update(user);
            return userUpdateResult;
        }

        public bool IsAddedRole(User user, string RoleId)
        {
            Role role = user.Role.Where(p => p._id == RoleId).First();
            if (role != null)
            {
                return true;
            }

            return false;
        }
    }
}
