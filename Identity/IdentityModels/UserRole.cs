using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityModels
{
    public class UserRole : EntityBase
    {

        public long User_Id { get; set; }
        public long Role_Id { get; set; }
    }
}
