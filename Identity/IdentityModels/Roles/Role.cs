using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityModels.Roles
{
    public class Role : EntityBase
    {
        public long User_Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
