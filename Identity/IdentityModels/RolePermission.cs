using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityModels
{
    public class RolePermission : EntityBase
    {

        public Guid PermissionId { get; set; }
        public Guid RoleId { get; set; }
    }
}
