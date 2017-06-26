using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityModels
{
    public class UserRole : EntityBase
    {

        public Guid User_Id { get; set; }
        public Guid Role_Id { get; set; }
    }
}
