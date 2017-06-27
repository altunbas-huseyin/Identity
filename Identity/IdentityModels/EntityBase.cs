using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace IdentityModels
{
    public abstract class EntityBase
    {
        [Key]
        public long Id { get; set; }
        public DateTime Create_Date { get; set; }
        public DateTime Update_Date { get; set; }
        public long Status_Id { get; set; }

        public EntityBase()
        {
           
            //this.Id = Guid.NewGuid().ToString();
           //Guid dd = IdentityHelper.SequentialGuidGenerator.NewSequentialGuid(IdentityHelper.SequentialGuidType.SequentialAsString);
        }

    }
}
