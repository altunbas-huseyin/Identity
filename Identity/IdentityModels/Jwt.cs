using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityModels
{
    public class Jwt : EntityBase
    {
        public long User_Id { get; set; }
        public string Token { get; set; }
        public DateTime Dead_Line { get; set; }
    }
}
