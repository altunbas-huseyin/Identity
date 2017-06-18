using IdentityModels;
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
