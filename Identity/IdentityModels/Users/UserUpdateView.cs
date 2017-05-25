using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityModels.Users
{
    public class UserUpdateView
    {
        public string _id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string FirmName { get; set; }
        public dynamic Extra1 { get; set; }
        public dynamic Extra2 { get; set; }
    }
}
