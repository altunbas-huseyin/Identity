using IdentityModels;
using IdentityModels.RolePermissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdentityRepository
{
    public class RolePermissionRepo
    {
        private MongoDbRepository<RolePermission> context = new MongoDbRepository<RolePermission>();
        private StatusRepo statusRepo = new StatusRepo();

        public bool Insert(RolePermission rolePermission)
        {
            RolePermission _role = this.GetByUserIdAndPermissionId(rolePermission.OwnerId, rolePermission.UserId, rolePermission._id);
            if (_role == null)
            {
                context.Insert(rolePermission);
            }
            return true;
        }

        public bool Update(RolePermission rolePermission)
        {
            return context.Update(rolePermission);
        }

        public bool Delete(string OwnerId, string UserId, String Id)
        {
            RolePermission _permission = this.GetById(OwnerId, UserId, Id);
            if (_permission == null)
            {
                return true;
            }
            return context.Delete(_permission);
        }

        public RolePermission GetById(string OwnerId, string UserId, String Id)
        {
            RolePermission rolePermission = context.SearchFor(p => p.OwnerId == OwnerId && p.UserId == UserId && p._id == Id).FirstOrDefault();
            return rolePermission;
        }

        public List<RolePermission> GetByOwnerIdAndUserId(string OwnerId, String UserId)
        {
            //OwnerId sahip kullanıcı yani AppAdmin rolüne sahip olan kullanıcıdır.
            List<RolePermission> rolePermissionList = context.SearchFor(p => p.OwnerId == OwnerId && p.UserId == UserId).ToList();
            return rolePermissionList;
        }

        public RolePermission GetByUserIdAndPermissionId(string OwnerId, string UserId, String RolePermissionId)
        {
            RolePermission rolePermission = context.SearchFor(p => p.OwnerId == OwnerId && p.UserId == UserId && p.PermissionId == RolePermissionId).FirstOrDefault();
            return rolePermission;
        }

        public bool AddUniqIndex()
        {
            List<string> list = new List<string>();
            list.Add("UserId");
            list.Add("PermissionId");
            list.Add("RoleId");
            bool result = context.AddUniqIndex(list.ToArray());
            return true;
        }
    }
}
