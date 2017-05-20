using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityModels.Users
{
    public class User : EntityBase
    {
        public string ProjectName { get; set; }
        public List<Role> Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirmCode { get; set; }
        public string FirmLogo { get; set; }
        public UserType Type { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string FirmName { get; set; }
        public dynamic Extra1 { get; set; }
        public dynamic Extra2 { get; set; }
    }
}
