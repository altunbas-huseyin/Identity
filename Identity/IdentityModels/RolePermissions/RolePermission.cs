using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityModels.RolePermissions
{
    public class RolePermission : EntityBase
    {
        //public string OwnerId { get; set; } //OwnerId sahip kullanıcı yani AppAdmin rolüne sahip olan kullanıcıdır.
        public string User_Id { get; set; }
        public string Permission_Id { get; set; }
        public string Role_Id { get; set; }
    }
}
