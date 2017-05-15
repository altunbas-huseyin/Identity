using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityModels
{
    public class UserRole : EntityBase
    {
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfDefault]
        public Guid UserId { get; set; }

        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfDefault]
        public Guid RoleId { get; set; }
    }
}
