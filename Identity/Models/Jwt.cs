﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Jwt
    {
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfDefault]
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime DeadLine { get; set; }
    }
}