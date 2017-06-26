using IdentityModels.Roles;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityModels.Users
{
    public class User : EntityBase
    {
        public string Parent_Id { get; set; } = "00000000-0000-0000-0000-000000000000";
        public List<Role> Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public dynamic Extra1 { get; set; }
        public dynamic Extra2 { get; set; }
    }
}
