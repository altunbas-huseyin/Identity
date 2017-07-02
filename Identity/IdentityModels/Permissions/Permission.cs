using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityModels.Permissions
{
    public class Permission : EntityBase
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
