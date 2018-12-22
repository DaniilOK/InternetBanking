using System;
using System.Linq;
using FluentValidation;
using IB.Repository.Interface;
using IB.Repository.Interface.Models;
using IB.Services.Interface.Commands;
using IB.Services.Interface.Interfaces;
using IB.Services.Interface.Models;
using IB.Services.Interface.Models.Enums;

namespace IB.Services
{
    public class AuthService : AppService, IAuthService
    {
        public AuthService(IUnitOfWork unitOfWork, IValidatorFactory validatorFactory) : base(unitOfWork, validatorFactory)
        {
        }

        public LoginModel Login(LoginCommand command)
        {
            EnsureIsValid(command);

            var login = command.Login;
            var password = command.Password;
            var user = UnitOfWork.UserRepository.FindByLogin(login);

            if (user != null)
            {
                if (user.Inactive)
                {
                    return new LoginModel(LoginResult.UserIsInactive);
                }

                return user.VerifyPassword(password)
                    ? Success(user)
                    : new LoginModel(LoginResult.IncorrectPassword);
            }

            return new LoginModel(LoginResult.IncorrectLogin);
        }

        public RegistrationModel Registration(RegistrationCommand command)
        {
            EnsureIsValid(command);
            // TODO: Check user email and user login before add to database.
            var user = User.CreateUser(command.Login, command.FirstName, command.LastName, command.Email,
                command.Password);
            UnitOfWork.UserRepository.Add(user);

            return new RegistrationModel(RegistrationResult.Success);
        }

        public BanResult BanUser(Guid id, bool isBan)
        {
            var user = UnitOfWork.UserRepository.Find(id);
            if (user == null)
            {
                return BanResult.NotFound;
            }

            user.Inactive = isBan;
            return BanResult.Success;
        }

        public EmailConfirmedResult ConfirmEmail(Guid id, bool confirm)
        {
            var user = UnitOfWork.UserRepository.Find(id);
            if (user == null)
            {
                return EmailConfirmedResult.NotFound;
            }

            user.IsEmailConfirmed = confirm;
            return EmailConfirmedResult.Success;
        }

        private LoginModel Success(User user)
        {
            var repository = UnitOfWork.UserPermissionRepository;
            var permissions = $":{string.Join(":", repository.FilterByUserId(user.Id).Select(x => x.PermissionId))}:";
            return new LoginModel(LoginResult.Success, user.Id.ToString(), user.FullName, permissions);
        }
    }
}
