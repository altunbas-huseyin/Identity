﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class UserType : EntityBase
    {

        public string Name { get; set; }
        

    }
}