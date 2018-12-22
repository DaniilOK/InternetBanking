using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using IB.Repository.Interface;
using IB.Services.Interface.Interfaces;
using IB.Services.Interface.Models;
using IB.Services.Mapping;

namespace IB.Services
{
    public class UserService : AppService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, IValidatorFactory validatorFactory) : base(unitOfWork, validatorFactory)
        {
        }

        public IReadOnlyCollection<ProfileModel> GetUsers()
        {
            var users = UnitOfWork.UserRepository.All().ToList();
            var ids = users.Select(u => u.Id);
            var permissions = UnitOfWork.UserPermissionRepository.Filter(x => ids.Contains(x.UserId))
                .GroupBy(x => x.UserId).ToList();
            var result = from usr in users
                         join perm in permissions on usr.Id equals perm.Key into p
                         from perm in p.DefaultIfEmpty()
                         select usr.ToProfileModel(perm?.Select(x => x.Permission.Name));
            return result.ToList();
        }

        public ProfileModel GetProfile(Guid id)
        {
            var user = UnitOfWork.UserRepository.Find(id);
            var permissions = UnitOfWork.UserPermissionRepository.FilterByUserId(id).Select(x => x.Permission.Name).ToList();

            if (user == null)
            {
                return null;
            }

            var result = new ProfileModel(user.Id, user.FirstName, user.LastName, user.Inactive, user.Email, user.IsEmailConfirmed, permissions);
            return result;
        }
    }
}
