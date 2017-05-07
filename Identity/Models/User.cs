using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class User : EntityBase
    {
        public enum UserType { Firm, User };

        public string ProjectName { get; set; }
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfDefault]
        public string ParentId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirmCode { get; set; }
        public string FirmLogo { get; set; }
        public UserType Type { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string FirmName { get; set; }

    }
}
