using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IB.Repository.Interface;
using IB.Repository.Interface.Models;
using IB.Repository.Interface.Repositories;
using IB.Services;
using IB.Services.Interface.Commands;
using IB.Services.Interface.Interfaces;
using IB.Services.Interface.Models.Enums;
using IB.Services.Interface.Validation;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace InternetBanking.UnitTests.Services
{
    [TestFixture]
    public class AuthServiceTests
    {
        private IAuthService _authService;
        private Mock<IUserRepository> mockUserRepository;
        private Mock<IUserPermissionRepository> mockUserPermissionRepository;

        private User user = new User
        {
            FirstName = "admin",
            LastName = "admin",
            Login = "admin",
            Email = "admin.admin@mail.ru",
            Inactive = false,
            IsEmailConfirmed = true,
            PasswordHash = "柒⍦괟勹ī㉞︂텋�揕퇭͹ꭌ㛿䩕㦓锉䈴邑禅軐麿崨ᓄద❆䤱驥栖ꜻ",
            PasswordSalt = "眡䏿圔쎣穤⸊鉜뷮㑃쮺䭊䧣՜쎫鮯䗎䑙ᷘ攋ᅭ섦楂憢�胔贛Ⴜ槒ᕎ἟铱⭏뇚禎�࠯쥤쓩₳绣䟉଩覟溳"
        };

        [OneTimeSetUp]
        public void InitializeOnce()
        {
            user.Id = Guid.NewGuid();
        }

        [SetUp]
        public void Initialize()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUserRepository = new Mock<IUserRepository>();
            mockUserPermissionRepository = new Mock<IUserPermissionRepository>();

            mockUnitOfWork.Setup(m => m.UserRepository).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(m => m.UserPermissionRepository).Returns(mockUserPermissionRepository.Object);

            var validatorFactory = new ValidatorFactory();

            _authService = new AuthService(mockUnitOfWork.Object, validatorFactory);
        }

        [Test]
        public void LoginTest_IncorrectLogin()
        {
            var loginCommand = new LoginCommand
            {
                Login = user.Login,
                Password = "d"
            };

            var result = _authService.Login(loginCommand);

            Assert.AreEqual(result.LoginResult, LoginResult.IncorrectLogin);
        }

        [Test]
        public void LoginTest_UserIsInactive()
        {
            var loginCommand = new LoginCommand
            {
                Login = "admin",
                Password = "admin"
            };
            user.Inactive = true;
            mockUserRepository.Setup(m => m.FindByLogin(It.Is<string>(x => x == user.Login))).Returns(user);

            var result = _authService.Login(loginCommand);

            Assert.AreEqual(result.LoginResult, LoginResult.UserIsInactive);
        }

        [Test]
        public void LoginTest_IncorrectPassword()
        {
            var loginCommand = new LoginCommand
            {
                Login = "admin",
                Password = "admin1"
            };

            user.Inactive = false;
            mockUserRepository.Setup(m => m.FindByLogin(It.Is<string>(x => x == user.Login))).Returns(user);

            var result = _authService.Login(loginCommand);

            Assert.AreEqual(result.LoginResult, LoginResult.IncorrectPassword);
        }

        [Test]
        public void LoginTest_Success()
        {
            var loginCommand = new LoginCommand
            {
                Login = "admin",
                Password = "admin"
            };
            var userPermissions = new List<UserPermission>
            {
                new UserPermission
                {
                    PermissionId = 1,
                    User = user,
                    UserId = user.Id,
                    Permission = new Permission
                    {
                        Id = 1,
                        Name = "administrator"
                    }
                }
            };
            user.Inactive = false;
            mockUserRepository.Setup(m => m.FindByLogin(It.Is<string>(x => x == user.Login))).Returns(user);
            mockUserPermissionRepository.Setup(m => m.FilterByUserId(It.Is<Guid>(x => x == user.Id))).Returns(userPermissions);

            var result = _authService.Login(loginCommand);

            Assert.AreEqual(result.LoginResult, LoginResult.Success);
            Assert.AreEqual(result.UserId, user.Id.ToString());
            Assert.AreEqual(result.Permissions, $":{string.Join(":", userPermissions.Select(x => x.Permission.Id))}:");
        }
    }
}
