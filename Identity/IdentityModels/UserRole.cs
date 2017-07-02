using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityModels
{
    public class UserRole : EntityBase
    {

        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
