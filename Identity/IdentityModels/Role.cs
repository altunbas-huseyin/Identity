﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityModels
{
    public class Role : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}