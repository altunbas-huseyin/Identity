using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public abstract class EntityBase
    {
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfDefault]
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfDefault]
        public Guid StatusId { get; set; }

        public EntityBase()
        {
            this.Id = Guid.NewGuid();
        }

    }
}
