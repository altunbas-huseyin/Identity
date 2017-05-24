using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityModels.Users
{
    public class UserView : EntityBase
    {
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }
        public string ParentId { get; set; }
        public List<Role> Role { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string FirmName { get; set; }
        public dynamic Extra1 { get; set; }
        public dynamic Extra2 { get; set; }
        public Jwt Jwt { get; set; }
    }
}
