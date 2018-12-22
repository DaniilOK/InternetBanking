using System;
using System.Collections.Generic;
using IB.Services.Interface.Models;

namespace IB.Services.Interface.Interfaces
{
    public interface IUserService
    {
        IReadOnlyCollection<ProfileModel> GetUsers();

        ProfileModel GetProfile(Guid id);
    }
}
