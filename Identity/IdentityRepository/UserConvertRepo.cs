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
            userView.Id = user.Id;
            userView.Role = user.Role;
            userView.Name = user.Name;
            userView.SurName = user.SurName;
            userView.Update_Date = user.Update_Date;
            userView.Parent_Id = user.Parent_Id;
            userView.Status_Id = user.Status_Id;

            return userView;
        }

    }
}
