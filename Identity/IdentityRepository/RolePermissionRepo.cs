﻿using IdentityModels;
using IdentityModels.Permissions;
using IdentityModels.RolePermissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdentityRepository
{
    public class RolePermissionRepo : BaseRepo<RolePermission>
    {
        
        private StatusRepo statusRepo = new StatusRepo();

        public bool Insert(RolePermission rolePermission)
        {
            RolePermission _role = this.GetByUserIdAndPermissionId( rolePermission.UserId, rolePermission._id);
            if (_role == null)
            {
                mongoContext.Insert(rolePermission);
            }
            return true;
        }

        public bool Update(RolePermission rolePermission)
        {
            return mongoContext.Update(rolePermission);
        }

        public bool Delete(string UserId, String RolePermissionId)
        {
            RolePermission _permission = this.GetById(UserId, RolePermissionId);
            if (_permission == null)
            {
                return true;
            }
            return mongoContext.Delete(_permission);
        }

        public RolePermission GetById(string UserId, String Id)
        {
            RolePermission rolePermission = mongoContext.SearchFor(p =>  p.UserId == UserId && p._id == Id).FirstOrDefault();
            return rolePermission;
        }

        public List<RolePermission> GetByUserIdAndRoleId( String UserId, string RoleId)
        {
            //OwnerId sahip kullanıcı yani AppAdmin rolüne sahip olan kullanıcıdır.
            List<RolePermission> rolePermissionList = mongoContext.SearchFor(p => p.UserId == UserId && p.RoleId==RoleId).ToList();
            return rolePermissionList;
        }

        public RolePermission GetByUserIdAndPermissionId( string UserId, String RolePermissionId)
        {
            RolePermission rolePermission = mongoContext.SearchFor(p =>  p.UserId == UserId && p.PermissionId == RolePermissionId).FirstOrDefault();
            return rolePermission;
        }

        public dynamic GetByUserIdWithJoinPermission(string UserId, string RoleId)
        {
            PermissionRepo permissionRepo = new PermissionRepo();
            List<Permission> permissionList = permissionRepo.GetByUserId(UserId);
            List<RolePermission> rolePermissionList = this.GetByUserIdAndRoleId(UserId, RoleId);

            var result = from rolePermission in rolePermissionList
                         join permission in permissionList
                              on rolePermission.PermissionId equals permission._id
                         select new
                         {
                             rolePermission._id,
                             rolePermission.PermissionId,
                             rolePermission.RoleId,
                             permission.Name,
                             permission.Description
                         };

            return result;
        }

        public dynamic GetByUserIdAndIdWithJoinPermission(string UserId, string RoleId, string Id)
        {
            PermissionRepo permissionRepo = new PermissionRepo();
            List<Permission> permissionList = permissionRepo.GetByUserId(UserId);
            List<RolePermission> rolePermissionList = new List<RolePermission>();
            rolePermissionList.Add(this.GetById(UserId, Id));

            var result = from rolePermission in rolePermissionList
                         join permission in permissionList
                              on rolePermission.PermissionId equals permission._id
                         select new
                         {
                             rolePermission._id,
                             rolePermission.PermissionId,
                             rolePermission.RoleId,
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
