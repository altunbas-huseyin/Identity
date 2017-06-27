using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityModels.RolePermissions
{
    public class Role_Permission : EntityBase
    {
        //public string OwnerId { get; set; } //OwnerId sahip kullanıcı yani AppAdmin rolüne sahip olan kullanıcıdır.
        public long User_Id { get; set; }
        public long Permission_Id { get; set; }
        public long Role_Id { get; set; }
    }
}
