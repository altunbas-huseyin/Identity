using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityModels
{
    public class RolePermission : EntityBase
    {
        public string OwnerId { get; set; } //OwnerId sahip kullanıcı yani AppAdmin rolüne sahip olan kullanıcıdır.
        public string UserId { get; set; }
        public string PermissionId { get; set; }
        public string RoleId { get; set; }
    }
}
