using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace IdentityModels
{
    public  class Customer : BaseEntityPG
    {
        [Key]
        public long Id { get; set; }
 
        [Required]
        public string Name { get; set; }
 
        [Required]
        public string Email { get; set; }

         [Required]
        public string Phone { get; set; }


        [Required]
        public string Address { get; set; }

    }
}
