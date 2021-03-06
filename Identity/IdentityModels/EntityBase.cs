﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityModels
{
    public abstract class EntityBase
    {
        [BsonId]
        public string _id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public Status Status { get; set; }

        public EntityBase()
        {
            this._id = Guid.NewGuid().ToString();
           
        }

    }
}
