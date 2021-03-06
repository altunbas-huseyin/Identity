﻿using IdentityModels;
using IdentityModels.Permissions;
using IdentityModels.RolePermissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdentityRepository
{
    public class PermissionRepo : BaseRepo<Permission>
    {
        
        private StatusRepo statusRepo = new StatusRepo();
        private RolePermissionRepo rolePermissionRepo = new RolePermissionRepo();
        public bool Insert(Permission permission)
        {
            // Permission _role = this.GetByName(permission.UserId, permission.Name);
            // if (_role == null)
            // {
            //     mongoContext.Insert(permission);
            // }
            mongoContext.Insert(permission);
            return true;
        }

        public bool Update(Permission permission)
        {
            
            return mongoContext.Update(permission);
        }

        public bool Delete(string UserId,String Id)
        {
            Permission _permission = this.GetById(UserId, Id);
            if (_permission == null)
            {
                return true;
            }
            return mongoContext.Delete(_permission);
        }

        public Permission GetByName(string UserId, String Name)
        {
            Permission permission = mongoContext.SearchFor(p => p.UserId == UserId && p.Name == Name).FirstOrDefault();
            return permission;
        }

        public Permission GetById(string UserId, String Id)
        {
            Permission permission = mongoContext.SearchFor(p => p.UserId == UserId && p._id == Id).FirstOrDefault();
            return permission;
        }

        public List<Permission> GetByUserId(string UserId)
        {
            List<Permission> permission = mongoContext.SearchFor(p => p.UserId == UserId).ToList();
            return permission;
        }

      
        public bool AddUniqIndex()
        {
            List<string> list = new List<string>();
            list.Add("UserId");
            list.Add("Name");

            bool result = mongoContext.AddUniqIndex(list.ToArray());
            return true;
        }
    }
}
