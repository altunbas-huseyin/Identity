using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityModels.Roles
{
    public class RoleUpdateView
    {
        public string _id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
