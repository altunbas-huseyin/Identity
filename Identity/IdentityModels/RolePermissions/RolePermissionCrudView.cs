using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityModels.RolePermissions
{
    public class RolePermissionCrudView
    {
       // public string Owner_Id { get; set; } //OwnerId sahip kullanıcı yani AppAdmin rolüne sahip olan kullanıcıdır.
        public long User_Id { get; set; }
        public string Permission_Id { get; set; }
        public string Role_Id { get; set; }
    }
}
