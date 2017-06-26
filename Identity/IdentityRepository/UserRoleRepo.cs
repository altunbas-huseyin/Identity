using IdentityModels.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IdentityModels.Roles;
using IdentityModels;

namespace IdentityRepository
{
    public class UserRoleRepo
    {
        
        private RoleRepo roleRepo = new RoleRepo();
        private UserRepo userRepo = new UserRepo();
        private Result result = new Result();
        public Result UserAddRole(string ParentId, string UserId, string RoleId)
        {
            User user = userRepo.GetById(ParentId, UserId);
            if (user == null)
            {
                result.AddError("Üye bulunamadı.");
                return result;
            }

            //Rol kullanıcıya daha önce atanmış ise bir işlem yapılmıyor
            bool resultIsAdded = IsAddedRole(user, RoleId);
            if (resultIsAdded)
            {
                result.AddError("Bu rol daha önce eklenmiş.");
                return result;
            }

            Role role = roleRepo.GetById(ParentId, RoleId);
            if (user.Role.Count < 1)
            { user.Role = new List<Role>(); }
            user.Role.Add(role);

            bool userUpdateResult = userRepo.Update(user);
            result.Status = userUpdateResult;
            return result;
        }

        public bool UserRemoveRole(string ParentId, string UserId, string RoleId)
        {
            User user = userRepo.GetById(ParentId, UserId);
            if (user == null)
            { return false; }

            bool resultIsAdded = IsAddedRole(user, RoleId);
            if (resultIsAdded)
            {
                Role role = user.Role.Where(p => p.Id == RoleId).First();
                user.Role.Remove(role);
                return userRepo.Update(user);
            }

            return false;
        }

        public bool IsAddedRole(User user, string RoleId)
        {
            Role role = user.Role.Where(p => p.Id == RoleId).FirstOrDefault();
            if (role != null)
            {
                return true;
            }

            return false;
        }
    }
}
