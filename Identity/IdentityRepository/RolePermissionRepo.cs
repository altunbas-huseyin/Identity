using IdentityModels;
using IdentityModels.Permissions;
using IdentityModels.RolePermissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace IdentityRepository
{
    public class RolePermissionRepo : BaseRepo<Role_Permission>
    {

        private StatusRepo statusRepo;
        PermissionRepo permissionRepo;
        public RolePermissionRepo(IConfiguration configuration) : base(configuration)
        {
            permissionRepo = new PermissionRepo(configuration);
            statusRepo = new StatusRepo(configuration);
        }

        public bool Insert(Role_Permission rolePermission)
        {
            Role_Permission _role = this.GetByUserIdAndPermissionId( rolePermission.User_Id, rolePermission.Id);
            if (_role == null)
            {
                mongoContext.Insert(rolePermission);
            }
            return true;
        }

        public bool Update(Role_Permission rolePermission)
        {
            return mongoContext.Update(rolePermission);
        }

        public bool Delete(long UserId, long RolePermissionId)
        {
            Role_Permission _permission = this.GetById(UserId, RolePermissionId);
            if (_permission == null)
            {
                return true;
            }
            return mongoContext.Delete(_permission);
        }

        public Role_Permission GetById(long UserId, long Id)
        {
            Role_Permission rolePermission = mongoContext.SearchFor(p =>  p.User_Id == UserId && p.Id == Id).FirstOrDefault();
            return rolePermission;
        }

        public List<Role_Permission> GetByUserIdAndRoleId(long UserId, long RoleId)
        {
            //OwnerId sahip kullanıcı yani AppAdmin rolüne sahip olan kullanıcıdır.
            List<Role_Permission> rolePermissionList = mongoContext.SearchFor(p => p.User_Id == UserId && p.Role_Id==RoleId).ToList();
            return rolePermissionList;
        }

        public Role_Permission GetByUserIdAndPermissionId(long UserId, long RolePermissionId)
        {
            Role_Permission rolePermission = mongoContext.SearchFor(p =>  p.User_Id == UserId && p.Permission_Id == RolePermissionId).FirstOrDefault();
            return rolePermission;
        }

        public dynamic GetByUserIdWithJoinPermission(long UserId, long RoleId)
        {
            List<Permission> permissionList = permissionRepo.GetByUserId(UserId);
            List<Role_Permission> rolePermissionList = this.GetByUserIdAndRoleId(UserId, RoleId);

            var result = from rolePermission in rolePermissionList
                         join permission in permissionList
                              on rolePermission.Permission_Id equals permission.Id
                         select new
                         {
                             rolePermission.Id,
                             rolePermission.Permission_Id,
                             rolePermission.Role_Id,
                             permission.Name,
                             permission.Description
                         };

            return result;
        }

        public dynamic GetByUserIdAndIdWithJoinPermission(long UserId, long RoleId, long Id)
        {
            
            List<Permission> permissionList = permissionRepo.GetByUserId(UserId);
            List<Role_Permission> rolePermissionList = new List<Role_Permission>();
            rolePermissionList.Add(this.GetById(UserId, Id));

            var result = from rolePermission in rolePermissionList
                         join permission in permissionList
                              on rolePermission.Permission_Id equals permission.Id
                         select new
                         {
                             rolePermission.Id,
                             rolePermission.Permission_Id,
                             rolePermission.Role_Id,
                             permission.Name,
                             permission.Description
                         };

            return result;
        }


        public bool AddUniqIndex()
        {
            List<string> list = new List<string>();
            list.Add("UserId");
            list.Add("PermissionId");
            list.Add("RoleId");
            bool result = mongoContext.AddUniqIndex(list.ToArray());
            return true;
        }
    }
}
