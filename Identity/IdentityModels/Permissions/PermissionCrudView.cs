using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityModels.Permissions
{
    public class PermissionCrudView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
