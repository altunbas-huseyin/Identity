using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityModels
{
    public abstract class EntityBase
    {
        
        public string Id { get; set; }
        public DateTime Create_Date { get; set; }
        public DateTime Update_Date { get; set; }
        public string Status_Id { get; set; }

        public EntityBase()
        {
           // this._id = Guid.NewGuid().ToString();
           
        }

    }
}
