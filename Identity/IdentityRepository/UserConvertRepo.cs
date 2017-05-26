using IdentityModels.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityRepository
{
    public class UserConvertRepo
    {
        public UserView UserToUserView(User user)
        {
            UserView userView = new UserView();
            userView.Email = user.Email;
            userView.Extra1 = user.Extra1;
            userView.Extra2 = user.Extra2;
            userView._id = user._id;
            userView.Role = user.Role;
            userView.Name = user.Name;
            userView.SurName = user.SurName;
            userView.UpdateDate = user.UpdateDate;
            userView.ParentId = user.ParentId;
            userView.Status = user.Status;

            return userView;
        }

    }
}
