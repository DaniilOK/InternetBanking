using System;
using System.Collections.Generic;
using System.Text;
using IB.Repository.Interface.Models;
using IB.Services.Interface.Models;

namespace IB.Services.Mapping
{
    public static class UserMapping
    {
        public static ProfileModel ToProfileModel(this User user, IEnumerable<string> permissions)
        {
            return new ProfileModel(user.Id, user.FirstName, user.LastName, user.Inactive, user.Email, user.IsEmailConfirmed, permissions);
        }
    }
}
