using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityModels
{
    public class RolePermission : EntityBase
    {
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfDefault]
        public Guid PermissionId { get; set; }

        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfDefault]
        public Guid RoleId { get; set; }
    }
}
